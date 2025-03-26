using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class SettingPresenterCoreFactory : IPostInitializable
    {
        [Inject] SettingStarter _starter;
        [Inject] SettingExiter _ender;
        [Inject] SettingRootView _rootView;

        [Inject] SettingMenuInputProcessor _inputProcessor;
        [Inject] SettingUiModel _uiModel;

        [Inject] ProfileMenuModel _profileMenuModel;
        [Inject] AdvancedMenuModel _advancedTabModel;

        [Inject] SettingTabManager _tabManager;

        [Inject] IDisposablePure _disposablePure;

        public void PostInitialize()
        {
            var presenter = new UiPresenterCore(
                _starter,
                _ender,
                _rootView,
                _inputProcessor,
                _uiModel,
                new List<IUiMenuModel> { _profileMenuModel,_advancedTabModel },
                _tabManager,
                _disposablePure
                );
            presenter.Present();
        }
    }
}