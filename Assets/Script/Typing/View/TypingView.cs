using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using TMPro;

namespace gaw241201.View
{
    public class TypingView : ITypingView
    {
        [Inject] TypingTextView _item;
        [Inject] IKeyInputJudger _keyInputJudger;
        [Inject] IQuestionTextGenerator _questionTextGenerator;
        [Inject] ITypingViewInitializer _viewInitializer;
        [Inject] ICorrectInputHundlable _correctInputHundlable;

        private List<char> _questionCharList = new List<char>();

        private int _charIndex;
        bool _isEndLoop = false;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;



        public async UniTask Enter(TypingViewArgs args)
        {
            Log.Comment("TypingView開始");

            //初期設定
            _viewInitializer.InitializeView(args, out _isEndLoop, out _questionCharList, out _charIndex);
            
            var v = args.CancellationToken.Register(OnExit);

            //すべての文字が終わるまで待って、処理を返す
            while (!_isEndLoop)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: args.CancellationToken);
                CheckInput();
            }

            v.Dispose();
            OnExit();
        }

        public void EndLoop()
        {
            _isEndLoop = false;
        }


        void CheckInput()
        {
            for (int i = 0; i < Input.inputString.Length; i++)
            {
                if (_keyInputJudger.IsKeyInputCorrect(Input.inputString[i], _charIndex, _questionCharList))
                {
                    _charIndex++;
                    _correctInputHundlable.OnCorrectInput(_questionCharList, _charIndex, out _isEndLoop);
                }
            }
        }


        private void OnExit()
        {
            Log.Comment("TypingView終了");
            _item.ResetText();
            _exited.OnNext(Unit.Default);
        }

      
    }
}