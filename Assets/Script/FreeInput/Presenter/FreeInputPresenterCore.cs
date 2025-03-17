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
    public class FreeInputPresenterCore : IFreeInputPresenterCore
    {
        //Model
        public FreeInputIndexer FreeInputIndexer { get; private set; }
        public FreeInputUnfixedText FreeInputUnfixedText { get; private set; }
        public IFreeInputCharHundler FreeInputCharHundler { get; private set; }
        public IFreeInputGateModel FreeInputGateModel { get; private set; }

        //View
        public IFreeInputDisplayView FreeInputTextDisplayView { get; private set; }
        public IFreeInputProcessor FreeInputProcessor { get; private set; }
        public FreeInputEntererView FreeInputEntererView { get; private set; }

        public IDisposablePure Disposable { get; private set; }

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
            FreeInputUnfixedText = freeInputUnfixedText;
            FreeInputIndexer = freeInputIndexer;
            FreeInputProcessor = freeInputProcessor;
            FreeInputCharHundler = freeInputCharHundler;
            FreeInputGateModel = stringDecidable;
            FreeInputTextDisplayView = playerNameDisplayView;
            FreeInputEntererView = freeInputEntererView;
            Disposable = disposable;
        }

        public void ActivatePresenter()
        {
            //開始、終了処理
            FreeInputGateModel.Entered.Subscribe(_ => FreeInputEntererView.Enter().Forget()).AddTo(Disposable);
            FreeInputGateModel.Exited.Subscribe(_ => FreeInputEntererView.Exit()).AddTo(Disposable);

            FreeInputUnfixedText.Updated.Subscribe(FreeInputTextDisplayView.SetCharacter).AddTo(Disposable);
            FreeInputIndexer.Focused.Subscribe(FreeInputTextDisplayView.Focus).AddTo(Disposable);
            FreeInputIndexer.Unfocused.Subscribe(FreeInputTextDisplayView.Unfocus).AddTo(Disposable);

            //InputProcessorとCharHundlerの紐づけ
            FreeInputProcessor.KeyEntered.Subscribe(FreeInputCharHundler.CatchChar).AddTo(Disposable);
            FreeInputProcessor.Decided.Subscribe(_ => FreeInputCharHundler.TryEnd()).AddTo(Disposable);
            FreeInputProcessor.Deleted.Subscribe(_ => FreeInputCharHundler.Delete()).AddTo(Disposable);

            FreeInputCharHundler.Ended.Subscribe(FreeInputGateModel.Decide).AddTo(Disposable);

        }
    }
}