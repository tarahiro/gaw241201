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
    public class StartMonitorModel : IFlowModel
    {
        Subject<CancellationToken> _entered = new Subject<CancellationToken>();
        public IObservable<CancellationToken> Entered => _entered;

        CancellationTokenSource _cts;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "‚ÌStartMonitorŠJŽn");
            _cts = new CancellationTokenSource();

            _entered.OnNext(_cts.Token);
        }
        public void ForceEndFlow()
        {
            
        }
    }
}