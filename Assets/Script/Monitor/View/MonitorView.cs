using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Threading;

namespace gaw241201.View
{
    public class MonitorView 
    {
        bool _isMonitored = false;

        Subject<Unit> _forceEnded = new Subject<Unit>();
        public IObservable<Unit> ForceEnded => _forceEnded;

        public async UniTask Monitor(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                if (KeyInputUtil.IsPressedCtrl())
                {
                    if (KeyInputUtil.IsKeyDown(KeyCode.C))
                    {
                        _forceEnded.OnNext(Unit.Default);
                    }
                }
            }
        }

        public void EndMonitor()
        {
            _isMonitored = false;
        }
    }
}