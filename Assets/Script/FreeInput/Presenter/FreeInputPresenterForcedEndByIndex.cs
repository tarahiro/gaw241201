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
    public class FreeInputPresenterForcedEndByIndex : IFreeInputPresenterCore
    {
        IFreeInputPresenterCore _underlying;
        FreeInputForceEnderByIndex _forceEnderByIndex;

        public FreeInputPresenterForcedEndByIndex(IFreeInputPresenterCore presenterCore, FreeInputForceEnderByIndex forceEnderByIndex)
        {
            _underlying = presenterCore;
            _forceEnderByIndex = forceEnderByIndex;
        }

        public void ActivatePresenter()
        {
            FreeInputIndexer.UpdateIndex.Subscribe(_forceEnderByIndex.OnUpdateIndex).AddTo(Disposable);
            FreeInputGateModel.Exited.Subscribe(_ => _forceEnderByIndex.OnExit()).AddTo(Disposable);

            _underlying.ActivatePresenter();
        }
        public FreeInputIndexer FreeInputIndexer => _underlying.FreeInputIndexer;
        public FreeInputUnfixedText FreeInputUnfixedText => _underlying.FreeInputUnfixedText;
        public IFreeInputCharHundler FreeInputCharHundler => _underlying.FreeInputCharHundler;
        public IFreeInputGateModel FreeInputGateModel => _underlying.FreeInputGateModel;

        //View
        public IFreeInputDisplayView FreeInputTextDisplayView => _underlying.FreeInputTextDisplayView;
        public IFreeInputProcessor FreeInputProcessor => _underlying.FreeInputProcessor;
        public FreeInputEntererView FreeInputEntererView => _underlying.FreeInputEntererView;

        public IDisposablePure Disposable => _underlying.Disposable;
    }
}