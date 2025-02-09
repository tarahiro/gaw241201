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
    public class FlagPublisher
    {
        [Inject] IPublisher<FlagConst.Key, string> _publisher;

        public void PublishEvent(FlagConst.Key key, string value)
        {
            _publisher.Publish(key, value);
        }
    }
}