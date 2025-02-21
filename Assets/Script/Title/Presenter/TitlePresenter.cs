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
    public class TitlePresenter : IPostInitializable
    {
        [Inject] TitleEnterModel _enterModel;
        [Inject] TitleExitModel _exitModel;
        [Inject] TitleRootView _rootView;
        [Inject] TitleInputView _inputView;

        [Inject] IDisposablePure _compositeDisposable;

        public void PostInitialize()
        {
            _enterModel.Entered.Subscribe(_ =>  _rootView.Enter().Forget()).AddTo(_compositeDisposable);
            _inputView.Decided.Subscribe(_ => _exitModel.ExitTitle()).AddTo(_compositeDisposable);
            _exitModel.Exited.Subscribe(_ => _rootView.Exit().Forget()).AddTo(_compositeDisposable);
        }
    }
}