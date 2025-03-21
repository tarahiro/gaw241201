using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class IndexVariantHundlerUiMenu : IIndexVariantHundler
    {
        MenuView _menuView;

        public IndexVariantHundlerUiMenu(MenuView menuView)
        {
            _menuView = menuView;
        }

        public int IndexVariant(Vector2Int cursorDirection)
        {
            int index = _menuView.Index;
            if (cursorDirection.y == 1)
            {
                index--;
            }

            if (cursorDirection.y == -1)
            {
                index++;
            }


            if (index < 0) index = _menuView.Count - 1;
            if (index >= _menuView.Count) index = 0;

            return index;
        }
    }
}