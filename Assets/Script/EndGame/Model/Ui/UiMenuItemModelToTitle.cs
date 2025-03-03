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
    public class UiMenuItemModelToTitle : IUiMenuItemModel
    {
        [Inject] SceneExecutor _executor;

        public UiMenuItemModelToTitle(SceneExecutor executor)
        {
            _executor = executor;
        }
        public void Enter()
        {
            _executor.ToTitle();
        }
    }
}