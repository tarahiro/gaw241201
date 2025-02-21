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
    public class EffectModel : ICategoryEnterableModel, IDisposable
    {
        [Inject] EffectArgsFactory _argsFactory;
        [Inject] ICancellationTokenPure _cts;

        Subject<EffectArgs> _entered = new Subject<EffectArgs>();
        public IObservable<EffectArgs> Entered => _entered;

        bool _isEnd;

        public async UniTask EnterFlow(string bodyId)
        {
            _cts.SetNew();
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

        public void Dispose()
        {
            /*
            Log.DebugLog("Dispose");
            if (_cts != null)
            {
                Log.DebugLog("Dispose");
                _cts.Dispose();
            }
            */
        }
    }
}