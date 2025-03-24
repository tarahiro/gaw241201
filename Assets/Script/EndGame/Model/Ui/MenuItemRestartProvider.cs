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
        SceneExecutor _executor;
        public MenuItemRestartProvider(SceneExecutor executor)
        {
            _executor = executor;
        }

        public IUiMenuItemModel Provide(int index)
        {
            switch (index)
            {
                case 0: return new UiMenuItemModelRestart(_executor);

                case 1: return new UiMenuItemModelToTitle(_executor);
                
                default:
                    Log.DebugAssert("–³Œø‚Èindex‚Å‚·:" + index);
                    return null;
            }

        }

        public int Count => 2;
    }
}