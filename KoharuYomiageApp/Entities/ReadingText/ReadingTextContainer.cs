using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KoharuYomiageApp.Entities.ReadingText
{
    public class ReadingTextContainer
    {
        readonly List<ReadingTextItem> _list = new();

        readonly object _syncObject = new();
        List<WeakReference<TaskCompletionSource<bool>>> _listForOverflow = new();

        List<WeakReference<TaskCompletionSource<ReadingTextItem>>> _listForTakeAsync = new();
        uint _maxCount = 10;

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

        void ClearWeakList()
        {
            _listForTakeAsync = _listForTakeAsync.Where(weak => weak.TryGetTarget(out var tcs) && !tcs.Task.IsCompleted)
                .ToList();
            _listForOverflow = _listForOverflow.Where(weak => weak.TryGetTarget(out var tcs) && !tcs.Task.IsCompleted)
                .ToList();
        }

        public void Add(ReadingTextItem item)
        {
            lock (_syncObject)
            {
                ClearWeakList();

                var listCount = _list.Count;
                if (listCount is 0)
                {
                    if (_listForTakeAsync.Count is 0)
                    {
                        _list.Add(item);
                    }
                    else
                    {
                        foreach (var weak in _listForTakeAsync)
                        {
                            if (weak.TryGetTarget(out var tcs))
                            {
                                tcs.SetResult(item);
                            }
                        }
                    }
                }
                else if (listCount > 0 && listCount < MaxCount)
                {
                    _list.Add(item);
                }
                else
                {
                    foreach (var weak in _listForOverflow)
                    {
                        if (weak.TryGetTarget(out var tcs))
                        {
                            tcs.SetResult(true);
                        }
                    }

                    _list.RemoveAt(0);
                    _list.Add(item);
                }
            }
        }

        public ValueTask<ReadingTextItem> TakeAsync(CancellationToken cancellationToken)
        {
            lock (_syncObject)
            {
                ClearWeakList();

                if (_list.Count is >0)
                {
                    var item = _list[0];
                    _list.RemoveAt(0);
                    return new ValueTask<ReadingTextItem>(item);
                }

                var tcs = new TaskCompletionSource<ReadingTextItem>(cancellationToken);
                _listForTakeAsync.Add(new WeakReference<TaskCompletionSource<ReadingTextItem>>(tcs));

                return new ValueTask<ReadingTextItem>(tcs.Task);
            }
        }

        public Task Overflow(CancellationToken cancellationToken)
        {
            lock (_syncObject)
            {
                ClearWeakList();

                var tcs = new TaskCompletionSource<bool>(cancellationToken);
                _listForOverflow.Add(new WeakReference<TaskCompletionSource<bool>>(tcs));

                return tcs.Task;
            }
        }
    }
}
