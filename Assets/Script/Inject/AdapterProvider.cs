using Cysharp.Threading.Tasks;
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
    public class AdapterProvider : IInitialAdapterProvider, IMainLoopEntererProvider
    {
        [Inject] TitleEnterModel _toTitle;
        [Inject] FakeLoopStarter _initialLoopStarter;

        public IAdapterManagerToModel ProvideInitialAdapter()
        {
            return _toTitle;
        }

        [Inject] 

        public IAdapterManagerToModel ProvideMainLoopAdapter()
        {
            return _initialLoopStarter;
        }
    }
}