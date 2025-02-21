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
    public class SettingExiter
    {
        Subject<SettingExitArgs> _settingExited = new Subject<SettingExitArgs>();
        public IObservable<SettingExitArgs> SettingExited => _settingExited;

        [Inject] ICancellationTokenPure _cts;
        public async UniTask Enter()
        {
            _cts.SetNew();
            _settingExited.OnNext(new SettingExitArgs(_cts.Token));
        }
    }
}