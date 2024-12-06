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

        [Inject] DeviceModelKeyReplacer modelFake;
        [Inject] DeviceModelLowerKeyReplacer modelLowerKeyReplacer;
        [Inject] DeviceTypeFake typeFake;
        [Inject] GraphicsName graphicsName;
        [Inject] GraphicsType GraphicsType;
        [Inject] GraphicsVendor GraphicsVendor;
        [Inject] GraphicsVersion GraphicsVersion;

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

                case FlagConst.MessageKey.DeviceModel:
                    return modelFake;

                case FlagConst.MessageKey.DeviceModelLower:
                    return modelLowerKeyReplacer;

                case FlagConst.MessageKey.DeviceType:
                    return typeFake;

                case FlagConst.MessageKey.GraphicsDeciveName:
                    return graphicsName;

                case FlagConst.MessageKey.GraphicsDeviceType:
                    return GraphicsType;

                case FlagConst.MessageKey.GraphicsDeviceVendor:
                    return GraphicsVendor;

                case FlagConst.MessageKey.GraphicsDeviceVersion:
                    return GraphicsVersion;


                default:
                    return _rowKeyReplacer;
            }
    
        }
    }
}