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
    public class ProfileMenuModel : ISettingTabModel
    {
        ISettingTabModel _settingTabModel;

        public int ItemIndex => _settingTabModel.ItemIndex;
        public int MaxItemRange => 5;

        Subject<int> _focusChanged = new Subject<int>();
        public IObservable<int> FocusChanged => _focusChanged;
        public void Initialize()
        {
            var tab = new SimpleSettingTabModel();
            tab.SetMaxItemRange(MaxItemRange);
            tab.FocusChanged.Subscribe(_focusChanged);

            _settingTabModel = tab;
        }

        public void MoveFocus(SettingConst.MenuDirection direction)
        {
            _settingTabModel.MoveFocus(direction);
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}