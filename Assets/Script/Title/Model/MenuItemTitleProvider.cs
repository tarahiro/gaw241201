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
        [Inject] UiMenuItemModelGameStart _gameStart;
        [Inject] UiMenuItemModelLanguage _language;


        public IUiMenuItemModel Provide(int index)
        {
            switch (index)
            {
                case 0: return _gameStart;

                case 1: return _language;

                default:
                    Log.DebugAssert("–³Œø‚Èindex‚Å‚·:" + index);
                    return null;
            }

        }

        public int Count => 2;
    }
}