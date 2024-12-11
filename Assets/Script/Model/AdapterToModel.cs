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
        [Inject] ILoadable _loadable;

        public async UniTask Enter()
        {
            Log.Comment("モデル開始");

            Log.Comment("ロード開始");
            _loadable.Load();

            Log.Comment("メインループ開始");
            _flowHundler.EnterMainLoop();
            //_flowHundler.EnterTypingTestFlow();
            //_flowHundler.FreeInputTestFlow();
        }
    }
}