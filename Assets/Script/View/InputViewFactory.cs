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
        public IInputView Create(IInputProcessable inputProcessable, ActiveLayerConst.InputLayer layer)
        {
            return new InputView(_subscriber, inputProcessable, layer);
        }
    }
}