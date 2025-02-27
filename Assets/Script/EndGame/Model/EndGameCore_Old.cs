using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class EndGameCore_Old : IEndGameCore
    {
        [Inject] ICancellationTokenPure _cancellationTokenSource;

        Subject<EndGameArgs> _entered = new Subject<EndGameArgs>();
        public IObservable<EndGameArgs> Entered => _entered;
        public void Enter(string bodyId)
        {
            _cancellationTokenSource.SetNew();
            _entered.OnNext(new EndGameArgs(_cancellationTokenSource.Token, EnumUtil.KeyToType<EndGameConst.Key>(bodyId)));

        }
    }
}