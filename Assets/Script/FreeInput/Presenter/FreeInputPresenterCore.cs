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
    public class FreeInputPresenterCore : IPostInitializable
    {
        //Model
        FreeInputUnfixedText _freeInputUnfixedText;
        FreeInputIndexer _freeInputIndexer;
        FreeInputProcessor _freeInputProcessor;
        FreeInputCharHundler _freeInputCharHundler;
        IStringDecidable _stringDecidable;

        //View
        FreeInputTextDisplayView _playerNameDisplayView;
       IDisposablePure _disposable;

        //要inject
        [Inject] IEnterTimingNotifiable _enterTimingNotifiable;
        [Inject] FreeInputEntererView _freeInputItemView;


        [Inject]
        public FreeInputPresenterCore(FreeInputUnfixedText freeInputUnfixedText, FreeInputIndexer freeInputIndexer, FreeInputProcessor freeInputProcessor, FreeInputCharHundler freeInputCharHundler, IStringDecidable stringDecidable, FreeInputTextDisplayView playerNameDisplayView, IDisposablePure disposable)
        {
            _freeInputUnfixedText = freeInputUnfixedText;
            _freeInputIndexer = freeInputIndexer;
            _freeInputProcessor = freeInputProcessor;
            _freeInputCharHundler = freeInputCharHundler;
            _stringDecidable = stringDecidable;
            _playerNameDisplayView = playerNameDisplayView;
            _disposable = disposable;
        }

        public void PostInitialize()
        {

            _freeInputUnfixedText.Updated.Subscribe(_playerNameDisplayView.SetCharacter).AddTo(_disposable);
            _freeInputIndexer.Focused.Subscribe(_playerNameDisplayView.Focus).AddTo(_disposable);
            _freeInputIndexer.Unfocused.Subscribe(_playerNameDisplayView.Unfocus).AddTo(_disposable);

            //InputProcessorとCharHundlerの紐づけ
            _freeInputProcessor.KeyEntered.Subscribe(_freeInputCharHundler.CatchChar).AddTo(_disposable);
            _freeInputProcessor.Decided.Subscribe(_ => _freeInputCharHundler.End()).AddTo(_disposable);
            _freeInputProcessor.Deleted.Subscribe(_ => _freeInputCharHundler.Delete()).AddTo(_disposable);

            //これをSetting側に逃がすかは要検討、IDecidableとかを用意して、FreeInput外のpresenterから繋げてもいいかも
            _freeInputCharHundler.Ended.Subscribe(_stringDecidable.Decide).AddTo(_disposable);

            //要Inject
            _enterTimingNotifiable.Entered.Subscribe(_ => _freeInputItemView.Enter().Forget()).AddTo(_disposable);
            _enterTimingNotifiable.Exited.Subscribe(_ => _freeInputItemView.Exit()).AddTo(_disposable);
        }
    }
}