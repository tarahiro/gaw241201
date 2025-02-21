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

        [Inject]  ICancellationTokenPure _cts;

        ISubscriber<FlagConst.Key, string> _subscriber;

        bool _isStartMonitorSetting = false;

        [Inject]
        IDisposablePure _disposablePure;

        [Inject]
        public MonitorModel(ISubscriber<FlagConst.Key, string> subscriber, IDisposablePure disposablePure, ICancellationTokenPure cancellationTokenPure)
        {
            _disposablePure = disposablePure;
            _subscriber = subscriber;
            _subscriber.Subscribe(FlagConst.Key.IsSettingEnable, OnFlagChanged).AddTo(_disposablePure);

            _cts = cancellationTokenPure;
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

            _cts.SetNew();
            _entered.OnNext(new MonitorArgs(bodyId, _cts.Token));
        }
    }
}