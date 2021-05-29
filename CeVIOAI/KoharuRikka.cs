using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CeVIOAI.Exceptions;
using NMeCab.Specialized;

namespace CeVIOAI
{
    public class KoharuRikka : IDisposable
    {
        const string CastName = "小春六花";
        readonly SemaphoreSlim _semaphore = new(1, 1);

        readonly Type _serviceControl2Type;
        readonly MeCabIpaDicTagger _tagger;
        readonly dynamic _talker;

        CancellationTokenSource? _nowSpeakingTaskCancellationTokenSource;

        /// <summary>
        ///     CeVIO AIを立ち上げ、小春六花にキャストを設定する
        /// </summary>
        /// <exception cref="DllNotFound">CeVIO AIが正しくインストールされておらずdllが見つからない</exception>
        /// <exception cref="CastNotFound">小春六花のトークボイスが見つからない</exception>
        public KoharuRikka()
        {
            var cevioAiPath = Environment.ExpandEnvironmentVariables("%ProgramW6432%") +
                @"\CeVIO\CeVIO AI\CeVIO.Talk.RemoteService2.dll";

            Assembly asm;
            try
            {
                asm = Assembly.LoadFrom(cevioAiPath);
            }
            catch (FileNotFoundException ex)
            {
                throw new DllNotFound(ex);
            }

            _serviceControl2Type = asm.GetType("CeVIO.Talk.RemoteService2.ServiceControl2")!;

            var startHostMethod =
                _serviceControl2Type.GetMethod("StartHost", BindingFlags.Static | BindingFlags.Public)!;
            startHostMethod.Invoke(null, new object?[] {false});

            var talkerAgent2Type = asm.GetType("CeVIO.Talk.RemoteService2.TalkerAgent2")!;
            var avilableCastsProp =
                talkerAgent2Type.GetProperty("AvailableCasts", BindingFlags.Static | BindingFlags.Public)!;
            dynamic availableCasts = avilableCastsProp.GetValue(null, null)!;
            if (!Array.Exists(availableCasts, (Predicate<string>)(cast => cast == CastName)))
            {
                throw new CastNotFound();
            }

            var talker2Type = asm.GetType("CeVIO.Talk.RemoteService2.Talker2")!;
            var talker2Constructor = talker2Type.GetConstructor(new[] {typeof(string)})!;
            _talker = talker2Constructor.Invoke(new object[] {CastName});

            _tagger = MeCabIpaDicTagger.Create();
        }

        /// <summary>
        ///     声量 0~100
        /// </summary>
        public uint Volume
        {
            get => _talker.Volume;
            set => _talker.Volume = value;
        }

        /// <summary>
        ///     話す速さ 0~100
        /// </summary>
        public uint Speed
        {
            get => _talker.Speed;
            set => _talker.Speed = value;
        }

        /// <summary>
        ///     声の高さ 0~100
        /// </summary>
        public uint Tone
        {
            get => _talker.Tone;
            set => _talker.Tone = value;
        }

        /// <summary>
        ///     声質 0~100
        /// </summary>
        public uint Alpha
        {
            get => _talker.Alpha;
            set => _talker.Alpha = value;
        }

        /// <summary>
        ///     抑揚 0~100
        /// </summary>
        public uint ToneScale
        {
            get => _talker.ToneScale;
            set => _talker.ToneScale = value;
        }

        /// <summary>
        ///     嬉しい 0~100
        /// </summary>
        public uint ComponentHappy
        {
            get => _talker.Components[0].Value;
            set => _talker.Components[0].Value = value;
        }

        /// <summary>
        ///     普通 0~100
        /// </summary>
        public uint ComponentNormal
        {
            get => _talker.Components[1].Value;
            set => _talker.Components[1].Value = value;
        }

        /// <summary>
        ///     怒り 0~100
        /// </summary>
        public uint ComponentAnger
        {
            get => _talker.Components[2].Value;
            set => _talker.Components[2].Value = value;
        }

        /// <summary>
        ///     哀しみ 0~100
        /// </summary>
        public uint ComponentSorrow
        {
            get => _talker.Components[3].Value;
            set => _talker.Components[3].Value = value;
        }

        /// <summary>
        ///     落ち着き 0~100
        /// </summary>
        public uint ComponentCalmness
        {
            get => _talker.Components[4].Value;
            set => _talker.Components[4].Value = value;
        }

        public void Dispose()
        {
            var closeHostMethod =
                _serviceControl2Type.GetMethod("CloseHost", BindingFlags.Static | BindingFlags.Public)!;
            closeHostMethod.Invoke(null, new object[] {0});
            _tagger.Dispose();
            _semaphore.Dispose();
        }

        async Task RawSpeak(string text, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                try
                {
                    dynamic state = _talker.Speak(text);
                    // 現状では読み上げテキストが空白や絵文字などで読み上げられない場合、終了を検知する方法がない
                    // Waitするとキャンセル処理が正しく働かない気がするが、次のSpeakでこの発話についてはキャンセルされるので多分大丈夫
                    // while (!state.IsCompleted)
                    // {
                    //     await Task.Delay(50, cancellationToken);
                    // }
                    state.Wait();
                }
                catch (OperationCanceledException)
                {
                    _talker.Stop();
                    throw;
                }
            }, cancellationToken);
        }

        /// <summary>
        ///     CeVIO AIにテキストを読ませる。
        ///     改行を含むテキストは改行ごとに500msの停止を含む。
        /// </summary>
        /// <param name="text">発話するテキスト</param>
        /// <param name="cancellationToken">CancellationToken</param>
        public async Task Speak(string text, CancellationToken cancellationToken = new())
        {
            // マルチスレッドでCeVIO AIが死ぬので、内部でCeVIO AIのAPIを叩くのは同時に1スレッドまでに制限している。
            // 発話中に次の発話リクエストが来た場合はキャンセルを飛ばしてから次の発話を実行している。

            _nowSpeakingTaskCancellationTokenSource?.Cancel(true);
            _nowSpeakingTaskCancellationTokenSource =
                CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var token = _nowSpeakingTaskCancellationTokenSource.Token;

            try
            {
                await _semaphore.WaitAsync(token);

                foreach (var line in text.Split('\n'))
                {
                    var nodes = _tagger.Parse(line);
                    var speakingText = "";
                    foreach (var node in nodes)
                    {
                        // CeVIO AI Talkは100文字以上連続して読み上げできない
                        if (speakingText.Length + node.Surface.Length >= 100)
                        {
                            await RawSpeak(speakingText, token);
                            speakingText = "";
                        }

                        speakingText += node.Surface;
                    }

                    await RawSpeak(speakingText, token);

                    // 行の終わりで一区切り
                    await Task.Delay(500, token);
                }
            }
            finally
            {
                _semaphore.Release();
            }

            _nowSpeakingTaskCancellationTokenSource = null;
        }
    }
}
