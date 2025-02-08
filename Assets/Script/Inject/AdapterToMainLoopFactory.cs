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
    public class AdapterToMainLoopFactory<T,U> : IAdapterToMainLoopFactory where T : IMainLoopStarter where U:ILoadable
    {
        [Inject] LifetimeScope[] _scope;

        public IAdapterManagerToModel CreateAdapter()
        {
            T _mainLoopHundler = default;
            U _loadable = default;

            _mainLoopHundler = InjectUtil.GetInstance<T>(_scope);
            _loadable = InjectUtil.GetInstance<U>(_scope);

            return new AdapterToMainLoop(_mainLoopHundler, _loadable);
        }
    }
}