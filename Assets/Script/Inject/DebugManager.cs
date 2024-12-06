using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.Inject
{
#if ENABLE_DEBUG
    public class DebugManager : ITickable, IStartable
    {
        [Inject] FlowHundler _flowHundler;
        [Inject] ISaveDeletable _saveDeletable;

        string _initialCategory;

        public void Start()
        {
            Log.DebugLog(SystemInfo.deviceName);
            Log.DebugLog(SystemInfo.deviceModel);
            Log.DebugLog(SystemInfo.deviceType);
            Log.DebugLog(SystemInfo.graphicsDeviceName);
            Log.DebugLog(SystemInfo.graphicsDeviceType);
            Log.DebugLog(SystemInfo.graphicsDeviceVendor);
            Log.DebugLog(SystemInfo.graphicsDeviceVersion);
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _flowHundler.ForceGetCurrentFlow().ForceEndFlow();
            }

            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                _saveDeletable.DeleteSaveData();
            }
        }
    }
#endif
}