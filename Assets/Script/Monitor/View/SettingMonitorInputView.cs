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

namespace gaw241201.View
{
    public class SettingMonitorInputView : IMonitorViewItem
    {
        [Inject] SettingMonitorDisplayView _settingMonitorDisplayView;

        Subject<Unit> _detected = new Subject<Unit>();
        public IObservable<Unit> Detected => _detected;

        public async UniTask Monitor(CancellationToken ct)
        {
            _settingMonitorDisplayView.Enter(ct).Forget();

            while (!ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
                if (KeyInputUtil.IsKeyDown(KeyCode.Escape))
                {
                    _detected.OnNext(Unit.Default);
                }
            }
        }
    }
}