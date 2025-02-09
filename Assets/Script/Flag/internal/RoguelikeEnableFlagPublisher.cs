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
    public class RoguelikeEnableFlagPublisher
    {
        [Inject] IPublisher<bool> _publisher;

        public void PublishEvent(bool b)
        {
            _publisher.Publish(b);
        }
    }
}