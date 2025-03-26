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
    public class UiMenuItemModelRestart: IUiMenuItemModel
    {
        [Inject] SceneExecutor _executor;

        public UiMenuItemModelRestart(SceneExecutor executor)
        {
            _executor = executor;
        }

        public void Enter()
        {
            Log.DebugLog("Restart");
            _executor.Restart();
        }
    }
}