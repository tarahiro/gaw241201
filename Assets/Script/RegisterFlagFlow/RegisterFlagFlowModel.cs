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
    public class RegisterFlagFlowModel : ICategoryEnterableModel
    {
        [Inject] RegisterFlagOrderProcessor _flagRegisterProcessor;

        public async UniTask EnterFlow(string bodyId)
        {
            _flagRegisterProcessor.ProcessRegisterOrder(EnumUtil.KeyToType<FlagConst.RegisterOrder>(bodyId));
        }
        public void ForceEndFlow()
        {

        }

    }
}