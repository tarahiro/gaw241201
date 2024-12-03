using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201
{
    public class ConfiscateModel : IFlowModel
    {
        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "‚ÌConfiscateŠJŽn");
        }

#if ENABLE_DEBUG
        public void ForceEndFlow()
        {

        }
#endif
    }
}