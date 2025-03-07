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
        [Inject] SettingUiModel   _uiModel;
        [Inject] AdvancedMenuModel _advancedTabModel;
        [Inject] AdvancedItemRoguelike _advancedItemRoguelike;

        [Inject] ISettingProfileItemListProvider _profileItemListProvider;

        [Inject] SettingMenuInputView _inputView;
        [Inject] SettingTabManager _tabManager;
        [Inject] SettingMenuInputProcessor _inputProcessor;
        [Inject] AdvancedItemProvider _advancedItemProvider;

        [Inject] PlayerNameDisplayModel _playerNameDisplayModel;
        [Inject] IPlayerNameSettable _playerNameSettable;

        [Inject] ProfileItemProvider _profileItemProvider;

        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {
            //InputのBlockと、Viewの紐づけ
            _inputView.BlockEnabled.Subscribe(_advancedItemProvider.TabBodyView.OnInputBlockEnabled).AddTo(_disposable);

            _uiModel.Initialize();

            //InputProcessorのタブ切り替えと、UIModelの紐づけ
            _inputProcessor.LrInputted.Subscribe(x => _uiModel.ChangeTab(x)).AddTo(_disposable);

            //Modelのタブ切り替えと、Viewの紐づけ
            _uiModel.TabChanged.Subscribe(_tabManager.ChangeTab).AddTo(_disposable);
            _uiModel.TabEntered.Subscribe(_tabManager.EnterTab).AddTo(_disposable);


            //高度な設定タブの有効無効と、Viewの紐づけ
            _advancedTabModel.SettedEnable.Subscribe(_advancedItemProvider.AdvancedTabIndex.OnSetEnabled).AddTo(_disposable);

            //高度な設定タブのローグライクの有効無効の紐づけ
            _advancedItemRoguelike.Entered.Subscribe(_ => _advancedItemProvider.RoguelikeCheck.Enter().Forget()).AddTo(_disposable);
            _advancedItemRoguelike.ValueChanged.Subscribe(_advancedItemProvider.RoguelikeCheck.SetValue).AddTo(_disposable);
            _advancedItemProvider.RoguelikeCheck.Exited.Subscribe(_ => _advancedItemRoguelike.End()).AddTo(_disposable);

            _inputView.BlockEnabled.Subscribe(_profileItemProvider.TabBodyView.OnInputBlockEnabled).AddTo(_disposable);

            //Modelと、Viewの紐づけ
            _profileItemListProvider.GetPlayerName().Entered.Subscribe(_ => _profileItemProvider.PlayerNameView.Enter().Forget()).AddTo(_disposable);
            _profileItemListProvider.GetPlayerName().Exited.Subscribe(_ => _profileItemProvider.PlayerNameView.Exit()).AddTo(_disposable);

            _playerNameDisplayModel.ValueChanged.Subscribe(_playerNameSettable.SetText).AddTo(_disposable);

            _advancedItemRoguelike.Initialize();
            _playerNameDisplayModel.Initialize();


        }
    }
}