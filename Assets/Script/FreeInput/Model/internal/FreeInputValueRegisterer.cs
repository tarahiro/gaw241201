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
{/*
    public class FreeInputValueRegisterer
    {
        [Inject] IGlobalFlagRegisterer _flagRegisterer;

        public void Register(FreeInputConst.FreeInputCategory bodyId, string value)
        {
            switch (bodyId)
            {
                case FreeInputConst.FreeInputCategory.TimeFreeInput:
                    _flagRegisterer.RegisterFlag(FlagConst.Key.InputTime, value);
                    break;

                case FreeInputConst.FreeInputCategory.NameFreeInput:
                    _flagRegisterer.RegisterFlag(FlagConst.Key.Name, value);
                    _flagRegisterer.RegisterFlag(FlagConst.Key.NameLower, value.ToLower());
                    break;

                case FreeInputConst.FreeInputCategory.BirthDateFreeInput:
                    _flagRegisterer.RegisterFlag(FlagConst.Key.BirthDate, value);
                    break;

                default:
                    Log.DebugAssert(bodyId + "は不正な値です");
                    break;
            }
        }
    }
    */
}