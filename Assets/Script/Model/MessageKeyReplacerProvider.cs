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
    public class MessageKeyReplacerProvider
    {
        [Inject] ApplicationTimeKeyReplacer _applicationTimeKeyReplacer;
        [Inject] DiffSecondKeyReplacer _diffSecondKeyReplacer;
        public IKeyReplacer GetKeyReplacer(string key)
        {
            switch (key)
            {
                case "ApplicationTime":
                    return _applicationTimeKeyReplacer;

                case "DiffSecond":
                    return _diffSecondKeyReplacer;

                default:
                    Log.DebugAssert(key + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    return null;
            }
    
        }
    }
}