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

        List<ActiveLayerConst.InputLayer> _layer = new List<ActiveLayerConst.InputLayer>() { ActiveLayerConst.InputLayer.None };

        public void PublishActiveLayer(ActiveLayerConst.InputLayer layer)
        {
            if (_layer[_layer.Count - 1] >= layer)
            {
                Log.DebugAssert("Ÿ‚ÌinputLayer‚ª¡‚Ì‚à‚ÌˆÈ‰º‚Å‚·");
            }
            else
            {
                _layer.Add(layer);
                _publisher.Publish(layer);
            }
        }

        public void ResetActiveLayer()
        {
            _layer.RemoveAt(_layer.Count - 1);
            _publisher.Publish(_layer[_layer.Count - 1]);
        }
    }
}