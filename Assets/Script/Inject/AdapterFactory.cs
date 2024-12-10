using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.Inject
{
    public class AdapterFactory<T> : IAdapterFactory where T : IMainLoopHundler
    {
        [Inject] HorrorStoryLifetimeScope _horrorStory;

        public IAdapterManagerToModel Create()
        {
            return new AdapterToModel(_horrorStory.Container.Resolve<T>(), _horrorStory.Container.Resolve<ILoadable>());
        }
    }
}