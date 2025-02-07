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
    public class AnimationPublisherFake
    {
        [Inject] IPublisher<string> _publisher;

        public void OnHorror(string s)
        {
            _publisher.Publish(s);
        }
    }
}