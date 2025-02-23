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
    public class IndexVariantHundlerSettings : IIndexVariantHundler
    {
        [Inject] SettingTabManager _tabManager;

        public int IndexVariant(Vector2Int cursorDirection)
        {
            int index = _tabManager.Current.MenuIndex;
            if(cursorDirection.y == 1)
            {
                index--;
            } 

            if(cursorDirection.y == -1)
            {
                index++;
            }


            if (index < 0) index = _tabManager.Current.MaxIndex - 1;
            if (index >= _tabManager.Current.MaxIndex) index = 0;

            return index;
        }
    }
}