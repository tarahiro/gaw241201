using Cysharp.Threading.Tasks;
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
    public class FreeInputPresenterRestrictedEnter : IFreeInputPresenterCore
    {
        IFreeInputPresenterCore _underlying;
        IEndableJudger _enterableJudger;
        IFreeInputEndableDisplayView _endableDisplayView;


        public FreeInputPresenterRestrictedEnter
            (IFreeInputPresenterCore presenterCore, 
            IEndableJudger endableJudger,
            IFreeInputEndableDisplayView endableDisplayView)
        {
            _underlying = presenterCore;
            _enterableJudger = endableJudger;
            _endableDisplayView = endableDisplayView;
        }

        public void ActivatePresenter()
        {
            FreeInputUnfixedText.Updated.Subscribe(_ => _enterableJudger.CatchUpdate())
                .AddTo(Disposable);
            FreeInputGateModel.Exited.Subscribe(_ => _enterableJudger.OnExit());
            _enterableJudger.EnterableStateUpdated.Subscribe(x => _endableDisplayView.Endable(x)).AddTo(Disposable);

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

        public IDisposablePure Disposable  => _underlying.Disposable;
    }
}