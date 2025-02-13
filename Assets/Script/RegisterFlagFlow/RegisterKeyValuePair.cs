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
    public class RegisterKeyValuePair : ICategoryEnterableModel
    {
        [Inject] IGlobalFlagRegisterer _registerer;

        public async UniTask EnterFlow(string bodyId)
        {
            string[] pair = bodyId.Split(',');
            _registerer.RegisterFlag(EnumUtil.KeyToType<FlagConst.Key>(pair[0]), pair[1]);
        }
        public void ForceEndFlow()
        {

        }

    }
}