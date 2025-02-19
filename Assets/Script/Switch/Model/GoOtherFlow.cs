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
    //アセンブリ変えるかも
    public class GoOtherFlow : ICategoryEnterableModel
    {
        [Inject] FlowSwitchPublisher _publisher;
        [Inject] TypedFlagContainer _typedFlagContainer;
        [Inject] IGlobalFlagProvider _globalFlagProvider;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "開始");
            _publisher.Publish(new FlowSwitchArgs(EnumUtil.KeyToType<FlowMasterConst.FlowMasterLabel>(
                bodyId), ""));
        }

        public void ForceEndFlow()
        {
        }

    }
}