using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using System.Threading;

namespace gaw241201.View
{
    public class TypingRoguelikeRootView :ITypingEnterView,  IHaltable
    {
        [Inject] TypingTextView _item;

        [Inject] TypingInputView _inputView;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;


        public async UniTask Enter(CancellationToken token)
        {
            Log.Comment("TypingRoguelikeViewŠJŽn");

            var v = token.Register(EndLoop);

            _inputView.Enter(token).Forget();
        }


        public void EndLoop()
        {
            _inputView.Exit();
            _item.ResetText();
            _inputView.Exit();
            _exited.OnNext(Unit.Default);
        }


        public void Halt()
        {
            EndLoop();
        }

    }
}