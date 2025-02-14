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
        [Inject] ProfileItemPlayerName _profileItemPlayerName;
        [Inject] FreeInputCharHundler _freeInputCharHundler;
        [Inject] FreeInputIndexer _freeInputIndexer;
        [Inject] FreeInputUnfixedText _freeInputUnfixedText;

        [Inject] SettingRootView _view;
        [Inject] SettingMenuInputView _inputView;
        [Inject] SettingTabManager _tabManager;
        [Inject] SettingMenuInputProcessor _inputProcessor;
        [Inject] FreeInputProcessor _freeInputProcessor;

        [Inject] ProfileItemProvider _profileItemProvider;
        [Inject] AdvancedItemProvider _advancedItemProvider;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _starter.SettingStarted.Subscribe(x => _view.Enter(x).Forget()).AddTo(_disposable);
            _exiter.SettingExited.Subscribe(x => _view.Exit(x).Forget()).AddTo(_disposable);

            _advancedTabModel.FocusChanged.Subscribe(x => _tabManager.Current.SetFocus(x).Forget()).AddTo(_disposable);
            _profileMenuModel.FocusChanged.Subscribe(x => _tabManager.Current.SetFocus(x).Forget()).AddTo(_disposable);

            _inputProcessor.IndexerMoved.Subscribe(x => _uiModel.Current.MoveFocus(x)).AddTo(_disposable);
            _inputProcessor.LrInputted.Subscribe(x => _uiModel.ChangeTab(x)).AddTo(_disposable);
            _inputProcessor.Decided.Subscribe(_ => _uiModel.Current.Decide()).AddTo(_disposable);

            _uiModel.Initialize();
            _uiModel.TabChanged.Subscribe(_tabManager.ChangeTab).AddTo(_disposable);
            _profileMenuModel.Decided.Subscribe(_ => _tabManager.Current.Decide(_).Forget()).AddTo(_disposable);
            _advancedTabModel.Decided.Subscribe(_ => _tabManager.Current.Decide(_).Forget()).AddTo(_disposable);


            _profileItemPlayerName.ValueChanged.Subscribe(_profileItemProvider.PlayerNameDisplayView.SetText).AddTo(_disposable);
            _profileItemPlayerName.Entered.Subscribe(_ => _profileItemProvider.PlayerNameView.Enter().Forget()).AddTo(_disposable);
            _profileItemPlayerName.Exited.Subscribe(_ => _profileItemProvider.PlayerNameView.Exit()).AddTo(_disposable);

            _freeInputIndexer.Focused.Subscribe(_profileItemProvider.PlayerNameDisplayView.Focus).AddTo(_disposable);
            _freeInputIndexer.Unfocused.Subscribe(_profileItemProvider.PlayerNameDisplayView.Unfocus).AddTo(_disposable);
            _freeInputProcessor.KeyEntered.Subscribe(_freeInputCharHundler.CatchChar).AddTo(_disposable);
            _freeInputProcessor.Decided.Subscribe(_ => _freeInputCharHundler.Decide()).AddTo(_disposable);
            _freeInputProcessor.Deleted.Subscribe(_ => _freeInputCharHundler.Delete()).AddTo(_disposable);
            _freeInputCharHundler.Decided.Subscribe(_profileItemPlayerName.Decide).AddTo(_disposable);
            _freeInputUnfixedText.Updated.Subscribe(_profileItemProvider.PlayerNameDisplayView.SetText).AddTo(_disposable);

            _advancedItemRoguelike.Entered.Subscribe(_ => _advancedItemProvider.RoguelikeCheck.Enter().Forget()).AddTo(_disposable);
            _advancedItemRoguelike.ValueChanged.Subscribe(_advancedItemProvider.RoguelikeCheck.SetValue).AddTo(_disposable);
            _advancedItemProvider.RoguelikeCheck.Exited.Subscribe(_ => _advancedItemRoguelike.End()).AddTo(_disposable);
            _advancedTabModel.SettedEnable.Subscribe(_advancedItemProvider.AdvancedTabIndex.OnSetEnabled).AddTo(_disposable);

            _profileItemPlayerName.Initialize();
            _advancedItemRoguelike.Initialize();
            
            _inputView.BlockEnabled.Subscribe(_profileItemProvider.TabBodyView.OnInputBlockEnabled).AddTo(_disposable);
            _inputView.BlockEnabled.Subscribe(_advancedItemProvider.TabBodyView.OnInputBlockEnabled).AddTo(_disposable);

        }
    }
}