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
using MessagePipe;

namespace gaw241201
{
    public class EndGameModel : ICategoryEnterableModel
    {
        [Inject] EndGameCoreModelProvider _coreProvider;


        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "ŠJŽn");
            _coreProvider.Provide(EnumUtil.KeyToType<EndGameConst.Key>(bodyId)).Enter(EnumUtil.KeyToType<EndGameConst.Key>(bodyId));
        }


        public void ForceEndFlow()
        {
        }

    }
}