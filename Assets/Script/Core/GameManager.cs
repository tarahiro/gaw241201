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
        [Inject] IInitialAdapterProvider _initialAdapterProvider;

        public void PostStart()
        {
            Log.Comment("ÉQÅ[ÉÄäJén");

            _initialAdapterProvider.ProvideInitialAdapter().Enter().Forget();
        }
    }
}