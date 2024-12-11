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
    public class TypingView
    {
        [Inject] IKeyInputJudger _keyInputJudger;
        [Inject] IQuestionTextGenerator _questionTextGenerator;
        [Inject] ITypingViewInitializer _viewInitializer;
        [Inject] TypingTextView _item;

        private List<char> _questionCharList = new List<char>();

        private int _charIndex;
        bool _isEndLoop = false;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public void Initialize()
        {
            _questionTextGenerator.Initialize();
        }

        public async UniTask Enter(TypingViewArgs args)
        {

            //初期設定
            _viewInitializer.InitializeView(args, out _isEndLoop, out _questionCharList, out _charIndex);
            UpdateQuestionText();

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

        void CheckInput()
        {
            for (int i = 0; i < Input.inputString.Length; i++)
            {
                if (_keyInputJudger.IsKeyInputCorrect(Input.inputString[i], _charIndex, _questionCharList))
                {
                    _charIndex++;
                    SoundManager.PlaySE("Key");
                    if (_questionCharList[_charIndex] == '@') // 「@」がタイピングの終わりの判定となる。
                    {
                        _isEndLoop = true;
                    }
                    else
                    {
                        UpdateQuestionText();
                    }
                }
            }
        }

        private void OnExit()
        {
            _item.ResetText();
            _exited.OnNext(Unit.Default);
        }

        void UpdateQuestionText()
        {
            _item.SetQuestionText(_questionTextGenerator.GenerateQuestionText(_questionCharList, _charIndex),_charIndex,_questionCharList.Count);
        }

      
    }
}