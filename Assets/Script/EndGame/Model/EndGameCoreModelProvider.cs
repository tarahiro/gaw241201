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
    public class EndGameCoreModelProvider
    {
        [Inject] EndGameCore_Old _oldModel;

        public IEndGameCore Provide(string bodyId)
        {
            //Ç∆ÇËÇ†Ç¶Ç∏å≈íËÇ≈èoÇ∑
            return _oldModel;
        }
    }
}