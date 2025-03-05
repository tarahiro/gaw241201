using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class FreeInputPresenterSetting : IPostInitializable
    {
        [Inject] ProfileItemPlayerName _profileItemPlayerName;
        [Inject] FreeInputCharHundler _freeInputCharHundler;
        [Inject] FreeInputIndexer _freeInputIndexer;
        [Inject] FreeInputUnfixedText _freeInputUnfixedText;

        [Inject] SettingFreeInputItemView _playerNameView;
        [Inject] FreeInputTextDisplayView _playerNameDisplayView;

        [Inject] IDisposablePure _disposable;

        [Inject] FreeInputProcessor _freeInputProcessor;
        public void PostInitialize()
        {

            //Settingのプレイヤー名の入力のModelと、Viewの紐づけ
            _freeInputProcessor.KeyEntered.Subscribe(_freeInputCharHundler.CatchChar).AddTo(_disposable);
            _freeInputProcessor.Decided.Subscribe(_ => _freeInputCharHundler.Decide()).AddTo(_disposable);
            _freeInputProcessor.Deleted.Subscribe(_ => _freeInputCharHundler.Delete()).AddTo(_disposable);
            _freeInputCharHundler.Decided.Subscribe(_profileItemPlayerName.Decide).AddTo(_disposable);

            
            //Settingのプレイヤー名のModelと、Viewの紐づけ
            _profileItemPlayerName.Entered.Subscribe(_ => _playerNameView.Enter().Forget()).AddTo(_disposable);
            _profileItemPlayerName.Exited.Subscribe(_ => _playerNameView.Exit()).AddTo(_disposable);

            _profileItemPlayerName.ValueChanged.Subscribe(_playerNameDisplayView.SetText).AddTo(_disposable);
            _freeInputIndexer.Focused.Subscribe(_playerNameDisplayView.Focus).AddTo(_disposable);
            _freeInputIndexer.Unfocused.Subscribe(_playerNameDisplayView.Unfocus).AddTo(_disposable);
            _freeInputUnfixedText.Updated.Subscribe(_playerNameDisplayView.SetCharacter).AddTo(_disposable);

            
            _profileItemPlayerName.Initialize();
        }
    }
}