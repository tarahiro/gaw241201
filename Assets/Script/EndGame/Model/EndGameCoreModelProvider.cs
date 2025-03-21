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
        [Inject] IEndGameUiGateModelProvider _provider;

        public IMenuModelGate Provide(EndGameConst.Key bodyId)
        {
            switch (bodyId)
            {
                case EndGameConst.Key.GameOverExhibition:
                    return _provider.GetGateModel();

                default:
                    return null;
            }

        }
    }
}