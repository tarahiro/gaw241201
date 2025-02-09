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
    public class DeviceModelLowerKeyReplacer : IKeyReplacer
    {
        public string ReplaceTo(FlagConst.MessageKey key)
        {
            Log.Comment("DeviceèëÇ´ä∑Ç¶");

            return SystemInfo.deviceModel.ToLower();
        }
    }
}