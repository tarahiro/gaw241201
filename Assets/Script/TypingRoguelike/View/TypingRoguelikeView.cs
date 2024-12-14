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
        [Inject] TypingTextView _item;

        private List<char> _questionCharList = new List<char>();

        private int _charIndex;
        bool _isEndLoop = false;

        Subject<char> _keyEntered = new Subject<char>();
        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<char> KeyEntered => _keyEntered;
        public IObservable<Unit> Exited => _exited;


        public async UniTask Enter(TypingViewArgs args)
        {
            Log.Comment("TypingRoguelikeView�J�n");

            //�����ݒ�
            _isEndLoop = false;

            var v = args.CancellationToken.Register(OnExit);

            //���ׂĂ̕������I���܂ő҂��āA������Ԃ�
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
                _keyEntered.OnNext(Input.inputString[i]);
            }
        }

        public void EndLoop()
        {
            _isEndLoop = true;
        }

        private void OnExit()
        {
            Log.Comment("TypingView�I��");
            _item.ResetText();
            _exited.OnNext(Unit.Default);
        }

        public void Halt()
        {
            _isEndLoop = true;
        }

    }
}