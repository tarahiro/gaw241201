using Cysharp.Threading.Tasks;
using gaw241201.Presenter;
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
        [Inject] PresenterCoreFactoryTitle _presenterCoreFactoryTitle;
        [Inject] FakeLoopStarter _initialLoopStarter;
        [Inject] MainLoopStarter _mainLoopStarter;
        [Inject] IGlobalFlagProvider _globalFlagProvider;

        [Inject] InitialParameter.StartOptionKey _startOptionKey;
        [Inject] bool _isFakeLoop;


        public IAdapterManagerToModel ProvideInitialAdapter()
        {
            if (GlobalStaticFlag.IsSkipTitle ||  _startOptionKey == InitialParameter.StartOptionKey.IsSkipTitle)
            {
                return ProvideMainLoopAdapter();
            }
            else
            {
                return _presenterCoreFactoryTitle.Provide();
            }
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
                if (GlobalStaticFlag.IsSkipTitle)
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
}