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
    public static class InjectUtil
    {
        public static T GetInstance<T>(LifetimeScope[] _scope)
        {
            foreach (var scope in _scope)
            {
                if (scope.Container.TryResolve<T>(out var instance))
                {
                    return instance;
                }
            }
            return default(T);
        }
    }
}