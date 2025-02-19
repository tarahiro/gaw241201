using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.SwitchConst;

namespace gaw241201
{
    public class ByStringGetterFactory : IByStringGetterFactory
    {
        [Inject] TypedFlagContainer _container;
        public IByStringGetter Create(ByCategory byCategory)
        {
            switch (byCategory)
            {
                case ByCategory.Typed:
                    return new TypedStringGetter(_container);

                default:
                    Log.DebugAssert(byCategory + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    return null;
            }
        }
    }
}