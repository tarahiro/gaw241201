using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.View
{
    public class TypingView : MonoBehaviour
    {
        [Inject] TypingItemView _currentItem;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public void Initialize()
        {
            _currentItem.Initialize();
        }

        public async UniTask Enter(TypingViewArgs args)
        {
            Log.Comment(args.SampleText + "ŠJŽn");

            var v = args.CancellationToken.Register(OnExit);
            await _currentItem.Enter(args);

            v.Dispose();
            if (!args.CancellationToken.IsCancellationRequested)
            {
                OnExit();
            }
        }

        private void OnExit()
        {
            _exited.OnNext(Unit.Default);
        }
    }
}