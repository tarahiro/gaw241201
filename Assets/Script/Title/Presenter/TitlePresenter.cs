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

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void PostInitialize()
        {
            Log.DebugLog("TitlePresenter: InitializeäJén");
            _enterModel.Entered.Subscribe(_ =>  _rootView.Enter().Forget()).AddTo(_compositeDisposable);
            _inputView.Decided.Subscribe(_ => _exitModel.ExitTitle()).AddTo(_compositeDisposable);
            _exitModel.Exited.Subscribe(_ => _rootView.Exit().Forget()).AddTo(_compositeDisposable);

            Log.DebugLog("TitlePresenter: InitializeèIóπ");
        }
    }
}