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
        Subject<ConfiscateConst.Type> _entered = new Subject<ConfiscateConst.Type>();
        public IObservable<ConfiscateConst.Type> Entered => _entered;


        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "‚ÌConfiscateŠJŽn");
            _entered.OnNext(EnumUtil.KeyToType<ConfiscateConst.Type>(bodyId));
        }

#if ENABLE_DEBUG
        public void ForceEndFlow()
        {

        }
#endif
    }
}