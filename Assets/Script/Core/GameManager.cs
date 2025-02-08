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
    public class GameManager : IPostStartable
    {
        [Inject] IGameInitializer _gameInitializer;
        [Inject] IInitialAdapterProvider _initialAdapterProvider;

        public void PostStart()
        {
            Log.Comment("ÉQÅ[ÉÄäJén");

            _gameInitializer.InitializeGame();
            _initialAdapterProvider.ProvideInitialAdapter().Enter().Forget();
        }
    }
}