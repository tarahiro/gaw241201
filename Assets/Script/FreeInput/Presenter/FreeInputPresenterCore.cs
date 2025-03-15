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
    public class FreeInputPresenterCore : IFreeInputPresenter
    {
        //Model
        internal FreeInputIndexer _freeInputIndexer;
        internal FreeInputUnfixedText _freeInputUnfixedText;
        internal IFreeInputCharHundler _freeInputCharHundler;
        internal IFreeInputGateModel _freeInputGateModel;

        //View
        internal IFreeInputDisplayView _freeInputTextDisplayView;
        internal IFreeInputProcessor _freeInputProcessor;
        internal FreeInputEntererView _freeInputEntererView;

        internal IDisposablePure _disposable;

        [Inject]
        public FreeInputPresenterCore(
            FreeInputIndexer freeInputIndexer, 
            FreeInputUnfixedText freeInputUnfixedText,
            IFreeInputCharHundler freeInputCharHundler,
            IFreeInputGateModel stringDecidable,
            IFreeInputDisplayView playerNameDisplayView,
            IFreeInputProcessor freeInputProcessor, 
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

        public void ActivatePresenter()
        {
            //�J�n�A�I������
            _freeInputGateModel.Entered.Subscribe(_ => _freeInputEntererView.Enter().Forget()).AddTo(_disposable);
            _freeInputGateModel.Exited.Subscribe(_ => _freeInputEntererView.Exit()).AddTo(_disposable);

            _freeInputUnfixedText.Updated.Subscribe(_freeInputTextDisplayView.SetCharacter).AddTo(_disposable);
            _freeInputIndexer.Focused.Subscribe(_freeInputTextDisplayView.Focus).AddTo(_disposable);
            _freeInputIndexer.Unfocused.Subscribe(_freeInputTextDisplayView.Unfocus).AddTo(_disposable);

            //InputProcessor��CharHundler�̕R�Â�
            _freeInputProcessor.KeyEntered.Subscribe(_freeInputCharHundler.CatchChar).AddTo(_disposable);
            _freeInputProcessor.Decided.Subscribe(_ => _freeInputCharHundler.TryEnd()).AddTo(_disposable);
            _freeInputProcessor.Deleted.Subscribe(_ => _freeInputCharHundler.Delete()).AddTo(_disposable);

            _freeInputCharHundler.Ended.Subscribe(_freeInputGateModel.Decide).AddTo(_disposable);

        }
    }
}