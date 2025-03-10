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

        public void Register(FreeInputConst.RegisterProcessKey bodyId, string value)
        {
            switch (bodyId)
            {
                case FreeInputConst.RegisterProcessKey.TimeFreeInput:
                    _flagRegisterer.RegisterFlag(FlagConst.Key.InputTime, value);
                    break;

                case FreeInputConst.RegisterProcessKey.NameFreeInput:
                    _flagRegisterer.RegisterFlag(FlagConst.Key.Name, value);
                    _flagRegisterer.RegisterFlag(FlagConst.Key.NameLower, value.ToLower());
                    break;

                case FreeInputConst.RegisterProcessKey.BirthDateFreeInput:
                    _flagRegisterer.RegisterFlag(FlagConst.Key.BirthDate, value);
                    break;

                default:
                    Log.DebugAssert(bodyId + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    break;
            }
        }
    }
}