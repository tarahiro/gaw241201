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
    public class MenuItemTitleProvider : IMenuItemProvider
    {

        public IUiMenuItemModel Provide(int index)
        {
            switch (index)
            {
                case 0: return new UiMenuItemModelGameStart();

                case 1: return new UiMenuItemModelLanguage();

                default:
                    Log.DebugAssert("–³Œø‚Èindex‚Å‚·:" + index);
                    return null;
            }

        }

        public int Count => 2;
    }
}