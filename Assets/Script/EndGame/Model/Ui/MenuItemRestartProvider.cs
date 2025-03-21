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
    public class MenuItemRestartProvider : IMenuItemRestartProvider
    {
        [Inject] SceneExecutor _executor;
        public IUiMenuItemModel Provide(int index)
        {
            switch (index)
            {
                case 0: return new UiMenuItemModelRestart(_executor);

                    case 1: return new UiMenuItemModelToTitle(_executor);
                default:
                    Log.DebugAssert("無効なindexです:" + index);
                    return null;
            }

        }

        public int Count => 2;
    }
}