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
    public class AdvancedMenuModel : IUiMenuModel
    {

        IUiMenuModel _menuModel;
        public int ItemIndex => _menuModel.ItemIndex;
        public int MaxItemRange => _menuModel.MaxItemRange;

        public IObservable<int> FocusChanged =>_menuModel.FocusChanged;
        public IObservable<int> Decided => _menuModel.Decided;

        [Inject]
        public AdvancedMenuModel(AdvancedMenuItemListFactory factory)
        {
            _menuModel = new UiMenuModel(factory.CreateList());
        }

        public void MoveFocus(int menuIndex)
        {
            _menuModel.MoveFocus(menuIndex);
        }

        public void Decide()
        {
            _menuModel.Decide();
        }

        public void Enter()
        {

        }

        public void Exit()
        {

        }
    }
}