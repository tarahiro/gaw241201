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
    public class CmdMonitorView : IMonitorViewItem
    {

        Subject<Unit> _detected = new Subject<Unit>();
        public IObservable<Unit> Detected => _detected;

        public async UniTask Monitor(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                if (KeyInputUtil.IsPressedCtrl())
                {
                    if (KeyInputUtil.IsKeyDown(KeyCode.C))
                    {
                        _detected.OnNext(Unit.Default);
                    }
                }
            }
        }
    }
}