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
    public class ProfileMenuItemListFactory
    {

        public List<IUiMenuItemModel> CreateList()
        {
            var _returnable = new List<IUiMenuItemModel>();
            _returnable.Add(new SettingUiMenuItemModelEmpty());
            _returnable.Add(new SettingUiMenuItemModelEmpty());
            _returnable.Add(new SettingUiMenuItemModelEmpty());
            _returnable.Add(new SettingUiMenuItemModelEmpty());
            _returnable.Add(new SettingUiMenuItemModelEmpty());
            return _returnable;
        }
    }
}