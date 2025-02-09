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

namespace gaw241201
{
    public class ActiveLayerPublisher
    {
        [Inject] IPublisher<ActiveLayerConst.InputLayer> _publisher;

        ActiveLayerConst.InputLayer _prevLayer = ActiveLayerConst.InputLayer.None;
        ActiveLayerConst.InputLayer _currentActiveLayer = ActiveLayerConst.InputLayer.None;

        public void Publish(ActiveLayerConst.InputLayer layer)
        {
            _prevLayer = _currentActiveLayer;
            _currentActiveLayer = layer;
            _publisher.Publish(layer);
        }

    }
}