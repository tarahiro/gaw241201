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
        Subject<SettingExitArgs> _settingStarted = new Subject<SettingExitArgs>();
        public IObservable<SettingExitArgs> SettingStarted => _settingStarted;

        CancellationTokenSource _cts;
        public async UniTask Enter()
        {
            _cts = new CancellationTokenSource();
            _settingStarted.OnNext(new SettingExitArgs(_cts.Token));
        }
    }
}