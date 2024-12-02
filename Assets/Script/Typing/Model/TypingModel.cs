using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.Model;
using System.Threading;

namespace gaw241201
{
    public class TypingModel : IFlowModel 
    {
        /*
        [Inject] ITypingMasterDataProvider masterDataProvider;
        [Inject] TypingModelArgsFactory _argsFactory;
        */


        CancellationTokenSource _cts = new CancellationTokenSource();

        public async UniTask EnterFlow(string bodyId)
        {

            Log.Comment(bodyId + "‚ÌTypingGroupŠJŽn");
        }

#if ENABLE_DEBUG
        public void ForceEndFlow()
        {
            _cts.Cancel();
        }

#endif
    }
}