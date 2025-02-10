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
    public class InputView : IInputView
    {
        [Inject] ISubscriber<ActiveLayerConst.InputLayer> _subscriber;
        [Inject] IInputProcessable _inputProcessable;
        [Inject] ActiveLayerPublisher _publisher;

        ActiveLayerConst.InputLayer _layer;

        bool _isEnable = false;


        [Inject]
        public InputView(ISubscriber<ActiveLayerConst.InputLayer> subscriber, ActiveLayerPublisher publisher, IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer)
        {
            _subscriber = subscriber;
            _publisher = publisher;
            _inputProcessable = inputProcessable;
            _layer = layer;

            _subscriber.Subscribe(OnActiveLayerChanged);
        }

        public async UniTask Enter(CancellationToken ct)
        {
            _isEnable = true;
            _publisher.PublishActiveLayer(_layer);

            while (_isEnable)
            {
                await UniTask.Yield(PlayerLoopTiming.Update,cancellationToken:ct);
                if (!_isBlocked())
                {
                    _inputProcessable.ProcessInput();
                }
            }
        }

        public void Exit()
        {
            _isEnable = false;
            _publisher.ResetActiveLayer();
        }

        ActiveLayerConst.InputLayer _activeLayer = ActiveLayerConst.InputLayer.None;

        bool _isBlocked()
        {
            return _layer < _activeLayer;
        }

        void OnActiveLayerChanged(ActiveLayerConst.InputLayer layer)
        {
            _activeLayer = layer;
        }
    }
}