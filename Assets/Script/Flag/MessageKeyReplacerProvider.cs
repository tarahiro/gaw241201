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
    public class MessageKeyReplacerProvider
    {
        [Inject] ApplicationTimeKeyReplacer _applicationTimeKeyReplacer;
        [Inject] DiffSecondKeyReplacer _diffSecondKeyReplacer;
        [Inject] DeviceKeyReplacer _deviceKeyReplacer;
        [Inject] DeviceLowerKeyReplacer _deviceLowerKeyReplacer;
        [Inject] RowKeyReplacer _rowKeyReplacer;

        public IKeyReplacer GetKeyReplacer(FlagConst.MessageKey key)
        {
            switch (key)
            {
                case FlagConst.MessageKey.ApplicationTime: 
                    return _applicationTimeKeyReplacer;

                case FlagConst.MessageKey.DiffSecond:
                    return _diffSecondKeyReplacer;

                case FlagConst.MessageKey.Device:
                    return _deviceKeyReplacer;

                case FlagConst.MessageKey.DeviceLower:
                    return _deviceLowerKeyReplacer;


                default:
                    return _rowKeyReplacer;
            }
    
        }
    }
}