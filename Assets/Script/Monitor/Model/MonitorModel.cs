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

namespace gaw241201
{
    public class MonitorModel
    {
        Subject<MonitorArgs> _entered = new Subject<MonitorArgs>();
        public IObservable<MonitorArgs> Entered => _entered;

        CancellationTokenSource _cts;

        ISubscriber<FlagConst.Key, string> _subscriber;

        bool _isStartMonitorSetting = false;

        [Inject]
        IDisposablePure _disposablePure;

        [Inject]
        public MonitorModel(ISubscriber<FlagConst.Key, string> subscriber, IDisposablePure disposablePure)
        {
            _disposablePure = disposablePure;
            _subscriber = subscriber;
            _subscriber.Subscribe(FlagConst.Key.IsSettingEnable, OnFlagChanged).AddTo(_disposablePure);
        }

        void OnFlagChanged(string value)
        {
            if (!_isStartMonitorSetting)
            {
                if(value == Tarahiro.Const.c_true)
                {
                    StartMonitor("Setting");
                    _isStartMonitorSetting = true;
                }
            }
        }

        public void StartMonitor(string bodyId)
        {
            Log.DebugLog(bodyId + "のMonitorをModel側で開始");

            _cts = new CancellationTokenSource();
            _entered.OnNext(new MonitorArgs(bodyId, _cts.Token));
        }
    }
}