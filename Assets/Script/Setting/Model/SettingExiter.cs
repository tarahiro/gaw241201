using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class SettingExiter : IMenuModelEndable
    {
        Subject<SettingExitArgs> _settingExited = new Subject<SettingExitArgs>();
        public IObservable<SettingExitArgs> MenuEnded => _settingExited;

        [Inject] ICancellationTokenPure _cts;
        public void MenuEnd()
        {
            _cts.SetNew();
            _settingExited.OnNext(new SettingExitArgs(_cts.Token));
        }
    }
}