using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Domain.ReadingText
{
    public class ReadingTextContainer : IDisposable
    {
        readonly List<ReadingTextItem> _list = new();
        readonly Subject<IEnumerable<ReadingTextItem>> _onItemsChange = new();
        readonly object _syncObject = new();

        List<TaskCompletionSource<bool>> _listForOverflow = new();
        List<TaskCompletionSource<ReadingTextItem>> _listForTakeAsync = new();
        uint _maxCount = 3;

        public uint MaxCount
        {
            get => _maxCount;
            set
            {
                lock (_syncObject)
                {
                    _maxCount = value;
                }
            }
        }

        public IObservable<IEnumerable<ReadingTextItem>> OnItemsChange => _onItemsChange;

        void ClearWeakList()
        {
            _listForTakeAsync = _listForTakeAsync.Where(tcs => !tcs.Task.IsCompleted).ToList();
            _listForOverflow = _listForOverflow.Where(tcs => !tcs.Task.IsCompleted).ToList();
        }

        public void Add(ReadingTextItem item)
        {
            lock (_syncObject)
            {
                ClearWeakList();

                _list.Add(item);
                _onItemsChange.OnNext(new List<ReadingTextItem>(_list));

                var listCount = _list.Count;
                if (listCount is not 0)
                {
                    foreach (var tcs in _listForTakeAsync)
                    {
                        tcs.SetResult(item);
                    }
                }

                if (listCount >= MaxCount)
                {
                    foreach (var tcs in _listForOverflow)
                    {
                        tcs.SetResult(true);
                    }
                }
            }
        }

        public ValueTask<ReadingTextItem> GetAsync(CancellationToken cancellationToken)
        {
            lock (_syncObject)
            {
                ClearWeakList();

                if (_list.Count is >0)
                {
                    var item = _list[0];
                    return new ValueTask<ReadingTextItem>(item);
                }

                var tcs = new TaskCompletionSource<ReadingTextItem>(cancellationToken);
                _listForTakeAsync.Add(tcs);

                return new ValueTask<ReadingTextItem>(tcs.Task);
            }
        }

        public void RemoveFirstItem()
        {
            if (_list.Count is not >0)
            {
                return;
            }

            _list.RemoveAt(0);
            _onItemsChange.OnNext(_list.ToList());
        }

        public Task Overflow(CancellationToken cancellationToken)
        {
            lock (_syncObject)
            {
                ClearWeakList();

                if (_list.Count >= MaxCount)
                {
                    return Task.CompletedTask;
                }

                var tcs = new TaskCompletionSource<bool>(cancellationToken);
                _listForOverflow.Add(tcs);

                return tcs.Task;
            }
        }

        public void Dispose()
        {
            _onItemsChange.Dispose();
        }
    }
}
