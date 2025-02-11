using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UnityEngine;
using VContainer;

namespace gaw241201
{
    public class NotifySave : ICategoryEnterableModel
    {
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;
        [Inject] ISavable _savable;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "ŠJŽn");
            _globalFlagRegisterer.RegisterFlag(FlagConst.Key.RestartFlow, bodyId);
            _savable.Save();
        }
        public void ForceEndFlow()
        {
        }

    }
}
