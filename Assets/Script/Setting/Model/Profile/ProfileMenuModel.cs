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
        ISettingProfileItemListProvider _factory;

        [Inject]
        public ProfileMenuModel(ISettingProfileItemListProvider factory)
        {
            Log.DebugLog("Construct:ProfileMenuModel");
            _factory = factory;

        }

        public void Initialize()
        {
            Log.DebugLog("Initialize:ProfileMenuModel");
            _menuModel = new UiMenuModel(_factory.ProvideList());
        }

        IUiMenuModel _menuModel;
        public int ItemIndex => _menuModel.ItemIndex;
        public int MaxItemRange => _menuModel.MaxItemRange;
        public bool IsEnable => _menuModel.IsEnable;
        public IObservable<int> FocusChanged => _menuModel.FocusChanged;
        public IObservable<int> Decided => _menuModel.Decided;
        public IObservable<int> Entered => _menuModel.Entered;
        public IObservable<Unit> Exited => _menuModel.Exited;

        public void MoveFocus(int i) => _menuModel.MoveFocus(i);

        public void Decide() => _menuModel.Decide();

        public void Enter() => _menuModel.Enter();

        public void Exit() => _menuModel.Exit();

    }
}