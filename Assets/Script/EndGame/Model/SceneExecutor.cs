using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using UnityEngine.SceneManagement;

namespace gaw241201
{
    public class SceneExecutor
    {
        [Inject] ScenePublisher _publisher;
        public void Restart()
        {
            GlobalStaticFlag.IsSkipTitle = true;
            _publisher.Publish();
            SceneManager.LoadScene("Main");
        }

        public void ToTitle()
        {
            GlobalStaticFlag.IsSkipTitle = false;
            _publisher.Publish();
            SceneManager.LoadScene("Main");
        }
    }
}