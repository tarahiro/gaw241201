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
    public class AdvancedTabModel : IUiMenuModel
    {
        [Inject] AdvancedMenuItemListFactory _factory;

        IUiMenuModel _settingTabModel;
        public int ItemIndex => _settingTabModel.ItemIndex;
        public int MaxItemRange => _settingTabModel.MaxItemRange;

        Subject<int> _focusChanged = new Subject<int>();
        public IObservable<int> FocusChanged => _focusChanged;

        Subject<int> _decided = new Subject<int>();
        public IObservable<int> Decided => _decided;
        public void Initialize()
        {
            var tab = new UiMenuModel(_factory.CreateList());
            tab.FocusChanged.Subscribe(_focusChanged);

            _settingTabModel = tab;
        }

        public void MoveFocus(int menuIndex)
        {
            _settingTabModel.MoveFocus(menuIndex);
        }

        public void Decide()
        {
            _settingTabModel.Decide();
            _decided.OnNext(ItemIndex);
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}