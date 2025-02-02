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
    public class ProfileMenuModel : IUiMenuModel
    {
        IUiMenuModel _settingTabModel;

        public int ItemIndex => _settingTabModel.ItemIndex;
        public int MaxItemRange => 5;

        Subject<int> _focusChanged = new Subject<int>();
        public IObservable<int> FocusChanged => _focusChanged;
        public void Initialize()
        {
            var tab = new UiMenuModel();
            tab.SetMaxItemRange(MaxItemRange);
            tab.FocusChanged.Subscribe(_focusChanged);

            _settingTabModel = tab;
        }

        public void MoveFocus(int menuIndex)
        {
            _settingTabModel.MoveFocus(menuIndex);
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}