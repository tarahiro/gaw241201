using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
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
        ActiveLayerConst.InputLayer _layer;

        bool _isEnable = false;


        [Inject]
        public InputView(ISubscriber<ActiveLayerConst.InputLayer> subscriber, IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer)
        {
            _subscriber = subscriber;
            _inputProcessable = inputProcessable;
            _layer = layer;

            _subscriber.Subscribe(OnActiveLayerChanged);
        }

        public async UniTask Enter()
        {
            _isEnable = true;
            while (_isEnable)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                if (!_isBlocked())
                {
                    _inputProcessable.ProcessInput();
                }
            }
        }

        public void Exit()
        {
            _isEnable = false;
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