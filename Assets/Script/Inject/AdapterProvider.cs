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
        [Inject] MainLoopStarter _mainLoopStarter;

        bool _isFakeLoop = false;

        public IAdapterManagerToModel ProvideInitialAdapter()
        {
            return _toTitle;
        }

        [Inject] 

        public IAdapterManagerToModel ProvideMainLoopAdapter()
        {
            if (!_isFakeLoop)
            {
                return _mainLoopStarter;
            }
            else
            {
                return _initialLoopStarter;
            }
        }
    }
}