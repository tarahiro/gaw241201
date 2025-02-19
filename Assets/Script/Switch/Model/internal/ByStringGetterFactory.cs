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
        [Inject] LanguageModel _languageModel;
        public IByStringGetter Create(ByCategory byCategory)
        {
            switch (byCategory)
            {
                case ByCategory.Typed:
                    return new StringGetterTyped(_container);

                case ByCategory.Language:
                    return new StringGetterLanguage(_languageModel);

                default:
                    Log.DebugAssert(byCategory + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    return null;
            }
        }
    }
}