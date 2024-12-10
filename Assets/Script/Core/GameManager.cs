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
        [Inject] IAdapterFactory _adapterFactory;
        public void PostStart()
        {
            Log.Comment("ÉQÅ[ÉÄäJén");

            _adapterFactory.Create().Enter().Forget();
        }
    }
}