using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Threading;

namespace gaw241201
{
    public class EffectModel : ICategoryEnterableModel
    {
        [Inject] EffectArgsFactory _argsFactory;

        Subject<EffectArgs> _entered = new Subject<EffectArgs>();
        public IObservable<EffectArgs> Entered => _entered;

        CancellationTokenSource _cts;
        bool _isEnd;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "のエフェクト開始");
            _cts = new CancellationTokenSource();
            _isEnd = false;
            _entered.OnNext(_argsFactory.Create(bodyId, _cts.Token));

            await UniTask.WaitUntil(() =>_isEnd);
        }

        public void End()
        {
            _isEnd = true;
        }


        public void ForceEndFlow()
        {
            _cts.Cancel();
        }

    }
}