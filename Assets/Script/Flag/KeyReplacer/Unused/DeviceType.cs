using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.DayTimeUtil;

namespace gaw241201
{
    public class DeviceType : IKeyReplacer
    {
        [Inject] IGlobalFlagProvider _flagProvider;
        public string ReplaceTo(FlagConst.MessageKey key)
        {
            return SystemInfo.deviceType.ToString();
        }
    }
}