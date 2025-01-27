using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.View;


namespace gaw241201.Presenter
{


    public class SettingPresenter : IPostInitializable
    {
        [Inject] SettingStarter _starter;
        [Inject] SettingExiter _exiter;

        [Inject] SettingUiModel _uiModel;
        [Inject] AdvancedTabModel _advancedTabModel;
        [Inject] ProfileMenuModel _profileMenuModel;

        [Inject] SettingRootView _view;
        [Inject] SettingInputView _inputView;
        [Inject] SettingTabManager _tabManager;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _starter.SettingStarted.Subscribe(x => _view.Enter(x).Forget()).AddTo(_disposable);
            _exiter.SettingStarted.Subscribe(x => _view.Exit(x).Forget()).AddTo(_disposable);

            _advancedTabModel.FocusChanged.Subscribe(_tabManager.ChangeItemFocusOnCurrentTab).AddTo(_disposable);
            _profileMenuModel.FocusChanged.Subscribe(_tabManager.ChangeItemFocusOnCurrentTab).AddTo(_disposable);

            _inputView.CursorMoved.Subscribe(_uiModel.MoveFocus).AddTo(_disposable);
            

            _uiModel.Initialize();
        }
    }
}