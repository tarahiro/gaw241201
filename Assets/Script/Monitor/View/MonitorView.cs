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
        [Inject] MonitorViewItemProvider _itemProvider;

        public void Enter(MonitorArgs args)
        {
            Log.DebugLog(args.BodyId + "のMonitorをView側で開始");
            _itemProvider.Create(args.BodyId).Monitor(args.CancellationToken).Forget();
        }

    }
}