using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using MessagePipe;

namespace gaw241201
{
    public class GazeMessagePublisher
    {
        [Inject] private IPublisher<GazeConst.GazingKey, Vector2> _publisher;

        public void PublishEvent(GazeConst.GazingKey key, Vector2 screenPosition)
        {
            Log.Comment("言語コマンド発行");
            _publisher.Publish(key, screenPosition);
        }
        
        public void OnType(Vector2 screenPosition)
        {
            PublishEvent(GazeConst.GazingKey.Main, screenPosition);
        }
    }
}