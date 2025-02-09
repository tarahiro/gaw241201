using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
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
    public class AdvancedMenuItemListFactory
    {
        [Inject] AdvancedItemRoguelike _advancedItemRogueLike;

        public List<IUiMenuItemModel> CreateList()
        {
            var _returnable = new List<IUiMenuItemModel>();
            _returnable.Add(_advancedItemRogueLike);
            _returnable.Add(new SettingUiMenuItemModelEmpty());
            return _returnable;
        }
    }
}