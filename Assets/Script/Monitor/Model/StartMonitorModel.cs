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
    public class StartMonitorModel
    {
        Subject<MonitorArgs> _entered = new Subject<MonitorArgs>();
        public IObservable<MonitorArgs> Entered => _entered;

        CancellationTokenSource _cts;

        public void StartMonitor(string bodyId)
        {
            Log.DebugLog(bodyId + "‚ÌMonitor‚ðModel‘¤‚ÅŠJŽn");

            _cts = new CancellationTokenSource();
            _entered.OnNext(new MonitorArgs(bodyId, _cts.Token));
        }
    }
}