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
    public class UiMenuModelTemplate : IUiMenuModel
    {
        IUiMenuModel _uiMenuModel;

        [Inject]
        public UiMenuModelTemplate(IMenuItemTemplateProvider provider)
        {
            List<IUiMenuItemModel> list = new List<IUiMenuItemModel>();

            //ここでprovider等を使ってitem追加。どのように追加するかはprovider次第

            _uiMenuModel = new UiMenuModel(list);
        }

        public int MaxItemRange => _uiMenuModel.MaxItemRange;
        public int ItemIndex => _uiMenuModel.ItemIndex;
        public bool IsEnable => _uiMenuModel.IsEnable;
        public IObservable<int> FocusChanged => _uiMenuModel.FocusChanged;
        public IObservable<int> Entered => _uiMenuModel.Entered;
        public IObservable<int> Decided => _uiMenuModel.Decided;
        public IObservable<Unit> Exited => _uiMenuModel.Exited;
        public void Enter() => _uiMenuModel.Enter();
        public void Exit() => _uiMenuModel.Exit();
        public void MoveFocus(int menuIndex) => _uiMenuModel.MoveFocus(menuIndex);
        public void Decide() => _uiMenuModel.Decide();
    }
}