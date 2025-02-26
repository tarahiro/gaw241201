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
    public class InputViewFactory
    {
        [Inject] ISubscriber<ActiveLayerConst.InputLayer> _subscriber;
        [Inject] ActiveLayerPublisher _publisher;
        public IInputView Create(IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer)
        {
            return new InputView(_subscriber, _publisher, inputProcessable, layer);
        }
    }
}