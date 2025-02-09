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
    public class SettingTabListFactory
    {
        [Inject] ProfileMenuModel _profileMenuModel;
        [Inject] AdvancedMenuModel _advancedTabModel;

        public List<IUiMenuModel> Create()
        {
            var returnableList = new List<IUiMenuModel>();
            returnableList.Add(_profileMenuModel);
            returnableList.Add(_advancedTabModel);

            return returnableList;
        }
    }
}