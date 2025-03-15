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
    public class FreeInputPresenterRestrictedEnter : IFreeInputPresenter
    {
        FreeInputPresenterCore _underlying;
        IEndableJudger _enterableJudger;
        IFreeInputEndableDisplayView _endableDisplayView;

        IDisposablePure _disposable => _underlying._disposable;

        public FreeInputPresenterRestrictedEnter
            (FreeInputPresenterCore presenterCore, 
            IEndableJudger endableJudger,
            IFreeInputEndableDisplayView endableDisplayView)
        {
            _underlying = presenterCore;
            _enterableJudger = endableJudger;
            _endableDisplayView = endableDisplayView;
        }

        public void ActivatePresenter()
        {
            _underlying._freeInputUnfixedText.Updated.Subscribe(_ => _enterableJudger.CatchUpdate())
                .AddTo(_disposable);
            _enterableJudger.EnterableStateUpdated.Subscribe(x => _endableDisplayView.Endable(x)).AddTo(_disposable);

            _underlying.ActivatePresenter();
        }
    }
}