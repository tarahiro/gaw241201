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
    public class AdapterFactory<T,U> : IAdapterFactory where T : IMainLoopStarter where U:ILoadable
    {
        [Inject] LifetimeScope[] _scope;

        public IAdapterManagerToModel Create()
        {
            T _mainLoopHundler = default;
            U _loadable = default;

            foreach(var scope in _scope)
            {
                if(scope.Container.TryResolve<T>(out var mainLoopHundler))
                {
                    _mainLoopHundler = mainLoopHundler;
                }
                if (scope.Container.TryResolve<U>(out var loadable))
                {
                    _loadable = loadable;
                }
            }

            return new AdapterToModel(_mainLoopHundler, _loadable);
        }
    }
}