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
using System.Threading;

namespace gaw241201.View
{
    /*
    public class TypingView : ITypingView
    {

        private List<char> _questionCharList = new List<char>();

        private int _charIndex;
        bool _isEndLoop = false;

        Subject<char> _keyEntered = new Subject<char>();
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<char> KeyEntered => _keyEntered;
        public IObservable<Unit> Exited => _exited;


        public async UniTask Enter(CancellationToken token)
        {
            Log.Comment("TypingRoguelikeView開始");

            //初期設定
            _isEndLoop = false;

            var v = token.Register(OnExit);

            //すべての文字が終わるまで待って、処理を返す
            while (!_isEndLoop)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
                CheckInput();
            }

            v.Dispose();
            OnExit();
        }
        void CheckInput()
        {
            for (int i = 0; i < Input.inputString.Length; i++)
            {
                _keyEntered.OnNext(Input.inputString[i]);
            }
        }

        public void EndLoop()
        {
            _isEndLoop = true;
        }

        private void OnExit()
        {
            Log.Comment("TypingView終了");
            _exited.OnNext(Unit.Default);
        }

    }
    */
}