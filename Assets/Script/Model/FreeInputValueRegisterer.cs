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

        public void Register(string bodyId, string value)
        {
            switch (bodyId)
            {
                case "TimeFreeInput":
                    _flagRegisterer.RegisterFlag("InputTime", value);
                    break;

                default:
                    Log.DebugAssert(bodyId + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    break;
            }
        }
    }
}