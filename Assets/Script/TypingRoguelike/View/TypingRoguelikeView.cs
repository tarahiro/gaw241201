using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TypingRoguelikeView : ITypingView, IHaltable
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


        public async UniTask Enter(TypingViewArgs args)
        {
            Log.Comment("TypingRoguelikeView開始");

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

        public void Halt()
        {
            _isEndLoop = true;
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
            Log.Comment("TypingView終了");
            _item.ResetText();
            _exited.OnNext(Unit.Default);
        }

        void UpdateQuestionText()
        {
            _item.SetQuestionText(_questionTextGenerator.GenerateQuestionText(_questionCharList, _charIndex), _charIndex, _questionCharList.Count);
        }
    }
}