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
    public class ScenePublisher
    {
        [Inject] IPublisher<Unit> _publisher;
        [Inject] IPublisher<SceneEndConst.SceneEndOrder, ISceneUnit> _scenePublisher;

        public void Publish()
        {
            _publisher.Publish(Unit.Default);
            for (int i = 0; i < EnumUtil.GetTypeNum<SceneEndConst.SceneEndOrder>(); i++)
            {
                _scenePublisher.Publish((SceneEndConst.SceneEndOrder)i, new SceneUnit());
            }
        }
    }
}