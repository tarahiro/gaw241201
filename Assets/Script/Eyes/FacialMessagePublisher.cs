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
    public class FacialMessagePublisher
    {
        [Inject] private IPublisher<GazeConst.GazingKey, ConversationViewConst.Facial> _publisher;

        public void PublishEvent(GazeConst.GazingKey key, ConversationViewConst.Facial facial)
        {
            _publisher.Publish(key,facial);
        }

    }
}