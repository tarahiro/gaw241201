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
        //Model
        [Inject] FreeInputPlayerName _profileItemPlayerName;
        [Inject] IStringDecidable _stringDecidable;
        [Inject] FreeInputCharHundler _freeInputCharHundler;
        [Inject] FreeInputIndexer _freeInputIndexer;
        [Inject] FreeInputUnfixedText _freeInputUnfixedText;
        [Inject] FreeInputProcessor _freeInputProcessor;




        //View
        [Inject] SettingFreeInputItemView _playerNameView;
        [Inject] FreeInputTextDisplayView _playerNameDisplayView;

        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {

            //InputProcessorとCharHundlerの紐づけ
            _freeInputProcessor.KeyEntered.Subscribe(_freeInputCharHundler.CatchChar).AddTo(_disposable);
            _freeInputProcessor.Decided.Subscribe(_ => _freeInputCharHundler.End()).AddTo(_disposable);
            _freeInputProcessor.Deleted.Subscribe(_ => _freeInputCharHundler.Delete()).AddTo(_disposable);

            
            //Modelと、Viewの紐づけ
            /*
            _profileItemPlayerName.Entered.Subscribe(_ => _playerNameView.Enter().Forget()).AddTo(_disposable);
            _profileItemPlayerName.Exited.Subscribe(_ => _playerNameView.Exit()).AddTo(_disposable);
            */
            //↑の2つはSetting側でいい。MenuItemの動きなため

            _profileItemPlayerName.ValueChanged.Subscribe(_playerNameDisplayView.SetText).AddTo(_disposable);

            _freeInputIndexer.Focused.Subscribe(_playerNameDisplayView.Focus).AddTo(_disposable);
            _freeInputIndexer.Unfocused.Subscribe(_playerNameDisplayView.Unfocus).AddTo(_disposable);
            _freeInputUnfixedText.Updated.Subscribe(_playerNameDisplayView.SetCharacter).AddTo(_disposable);

            //これをSetting側に逃がすかは要検討、IDecidableとかを用意して、FreeInput外のpresenterから繋げてもいいかも
            _freeInputCharHundler.Ended.Subscribe(_stringDecidable.Decide).AddTo(_disposable);

            _profileItemPlayerName.Initialize();
        }
    }
}