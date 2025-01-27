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
    public class SettingTabManager : MonoBehaviour
    {
       [SerializeField]  List<SettingTabView> _tabList;

        public SettingTabView Current { get; private set; }

        public void Enter(SettingEnterArgs args)
        {
            Current = _tabList[args.TabIndex];
        }

        public void ChangeItemFocusOnCurrentTab(int itemIndex)
        {
            Current.ChangeFocus(itemIndex);
        }
    }
}