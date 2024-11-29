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
    public class FreeInputValueRegisterer
    {
        [Inject] IGlobalFlagRegisterer _flagRegisterer;

        public void Register(FreeInputConst.Key bodyId, string value)
        {
            switch (bodyId)
            {
                case FreeInputConst.Key.TimeFreeInput:
                    _flagRegisterer.RegisterFlag(FlagConst.Key.InputTime, value);
                    break;

                default:
                    Log.DebugAssert(bodyId + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    break;
            }
        }
    }
}