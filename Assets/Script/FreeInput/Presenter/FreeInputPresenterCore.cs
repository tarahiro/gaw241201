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
    public class FreeInputPresenterCore
    {
        //Model
        FreeInputIndexer _freeInputIndexer;
        FreeInputUnfixedText _freeInputUnfixedText;
        IFreeInputCharHundler _freeInputCharHundler;
        IFreeInputGateModel _freeInputGateModel;

        //View
        IFreeInputTextDisplayView _freeInputTextDisplayView;
        IFreeInputProcessor _freeInputProcessor;
        FreeInputEntererView _freeInputEntererView;

        IDisposablePure _disposable;

        [Inject]
        public FreeInputPresenterCore(
            FreeInputIndexer freeInputIndexer, 
            FreeInputUnfixedText freeInputUnfixedText,
            FreeInputCharHundler freeInputCharHundler,
            IFreeInputGateModel stringDecidable, 
            FreeInputTextDisplayView playerNameDisplayView,
            FreeInputProcessor freeInputProcessor, 
             FreeInputEntererView freeInputEntererView, 
             IDisposablePure disposable)
        {
            _freeInputUnfixedText = freeInputUnfixedText;
            _freeInputIndexer = freeInputIndexer;
            _freeInputProcessor = freeInputProcessor;
            _freeInputCharHundler = freeInputCharHundler;
            _freeInputGateModel = stringDecidable;
            _freeInputTextDisplayView = playerNameDisplayView;
            _freeInputEntererView = freeInputEntererView;
            _disposable = disposable;
        }

        public void PostInitialize()
        {
            //開始、終了処理
            _freeInputGateModel.Entered.Subscribe(_ => _freeInputEntererView.Enter().Forget()).AddTo(_disposable);
            _freeInputGateModel.Exited.Subscribe(_ => _freeInputEntererView.Exit()).AddTo(_disposable);

            _freeInputUnfixedText.Updated.Subscribe(_freeInputTextDisplayView.SetCharacter).AddTo(_disposable);
            _freeInputIndexer.Focused.Subscribe(_freeInputTextDisplayView.Focus).AddTo(_disposable);
            _freeInputIndexer.Unfocused.Subscribe(_freeInputTextDisplayView.Unfocus).AddTo(_disposable);

            //InputProcessorとCharHundlerの紐づけ
            _freeInputProcessor.KeyEntered.Subscribe(_freeInputCharHundler.CatchChar).AddTo(_disposable);
            _freeInputProcessor.Decided.Subscribe(_ => _freeInputCharHundler.End()).AddTo(_disposable);
            _freeInputProcessor.Deleted.Subscribe(_ => _freeInputCharHundler.Delete()).AddTo(_disposable);

            _freeInputCharHundler.Ended.Subscribe(_freeInputGateModel.Decide).AddTo(_disposable);

        }
    }
}