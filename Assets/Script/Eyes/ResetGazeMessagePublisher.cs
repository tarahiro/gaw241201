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
    public class ResetGazeMessagePublisher
    {
        [Inject] private IPublisher<GazeConst.GazingKey, Unit> _publisher;

        public void PublishEvent(GazeConst.GazingKey key, Unit unit)
        {
            _publisher.Publish(key, unit);
        }
    }
}