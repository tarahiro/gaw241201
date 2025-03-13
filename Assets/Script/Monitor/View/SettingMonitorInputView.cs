using Cysharp.Threading.Tasks;
using MessagePipe;
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

        ISubscriber<ActiveLayerConst.InputLayer> _subscriber;

        public SettingMonitorInputView(SettingMonitorDisplayView settingMonitorDisplayView, ISubscriber<ActiveLayerConst.InputLayer> subscriber)
        {
            _settingMonitorDisplayView = settingMonitorDisplayView;
            _subscriber = subscriber;
            _subscriber.Subscribe(OnChangeActiveLayer);

        }

        public async UniTask Monitor(CancellationToken ct)
        {
            _settingMonitorDisplayView.Enter(ct).Forget();

            while (!ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, ct);
                if (KeyInputUtil.IsKeyDown(KeyCode.Escape))
                {
                    Log.DebugLog("EscapeをSettingMonitorで検知");
                    if (!IsBlocked())
                    {
                        Log.DebugLog("SettingMonitorがブロックされていないことを検知");
                        _detected.OnNext(Unit.Default);

                    }
                }
            }
        }

        ActiveLayerConst.InputLayer _layer = ActiveLayerConst.InputLayer.SettingMenu;
        ActiveLayerConst.InputLayer _activeLayer;

        void OnChangeActiveLayer(ActiveLayerConst.InputLayer activeLayer)
        {
            _activeLayer = activeLayer;
        }

        bool IsBlocked()
        {
            return !(_layer >= _activeLayer);
        }
    }
}