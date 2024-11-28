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
    public class AdapterToModel : IAdapterManagerToModel
    {
        [Inject] IMainLoopHundler _flowHundler;

        public async UniTask Enter()
        {
            Log.Comment("ƒ‚ƒfƒ‹ŠJŽn");

            await _flowHundler.EnterMainLoop();
        }
    }
}