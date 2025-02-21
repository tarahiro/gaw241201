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
    public class CancellationTokenPure : ICancellationTokenPure
    {
        CancellationTokenSource _cancellationTokenSource;
        [Inject] ISubscriber<SceneEndConst.SceneEndOrder, ISceneUnit> _subscriber;

        public void Cancel()
        {
            Log.DebugLog("CancellationTokenPure: Cancel");

            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }

        }
        public CancellationToken Token
        {
            get
            {
                return _cancellationTokenSource.Token;
            }
        }

        public CancellationTokenPure(ISubscriber<SceneEndConst.SceneEndOrder, ISceneUnit> subscriber,IDisposablePure disposable)
        {
            _subscriber = subscriber;
            _subscriber.Subscribe(SceneEndConst.SceneEndOrder.Initialize, _ => Cancel()).AddTo(disposable);
        }

        public void SetNew()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

    }
}