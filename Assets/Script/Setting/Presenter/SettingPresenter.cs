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

        [Inject] SettingUiModel   _uiModel;
        [Inject] AdvancedMenuModel _advancedTabModel;
        [Inject] ProfileMenuModel _profileMenuModel;
        [Inject] AdvancedItemRoguelike _advancedItemRoguelike;

        [Inject] SettingRootView _view;
        [Inject] SettingMenuInputView _inputView;
        [Inject] SettingTabManager _tabManager;
        [Inject] SettingMenuInputProcessor _inputProcessor;

        [Inject] SettingAdvancedItemProvider _advancedItemProvider;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _starter.SettingStarted.Subscribe(x => _view.Enter(x).Forget()).AddTo(_disposable);
            _exiter.SettingStarted.Subscribe(x => _view.Exit(x).Forget()).AddTo(_disposable);

            _advancedTabModel.FocusChanged.Subscribe(x =>  _tabManager.Current.SetFocus(x).Forget()).AddTo(_disposable);
            _profileMenuModel.FocusChanged.Subscribe(x => _tabManager.Current.SetFocus(x).Forget()).AddTo(_disposable);

            _inputProcessor.IndexerMoved.Subscribe(x =>  _uiModel.Current.MoveFocus(x)).AddTo(_disposable);
            _inputProcessor.LrInputted.Subscribe(x => _uiModel.ChangeTab(x)).AddTo(_disposable);
            _inputProcessor.Decided.Subscribe(_ => _uiModel.Current.Decide()).AddTo(_disposable);

            _uiModel.Initialize();
            _uiModel.TabChanged.Subscribe(_tabManager.ChangeTab).AddTo(_disposable);
            _profileMenuModel.Decided.Subscribe(_ =>  _tabManager.Current.Decide(_).Forget()).AddTo(_disposable);
            _advancedTabModel.Decided.Subscribe(_ => _tabManager.Current.Decide(_).Forget()).AddTo(_disposable);

            _advancedItemRoguelike.Entered.Subscribe(_ => _advancedItemProvider.RoguelikeCheck.Enter().Forget()).AddTo(_disposable);
            _advancedItemRoguelike.ValueChanged.Subscribe(_advancedItemProvider.RoguelikeCheck.SetValue).AddTo(_disposable);
            _advancedItemProvider.RoguelikeCheck.Exited.Subscribe(_ => _advancedItemRoguelike.End()).AddTo(_disposable);


            _advancedItemRoguelike.Initialize();
            
        }
    }
}