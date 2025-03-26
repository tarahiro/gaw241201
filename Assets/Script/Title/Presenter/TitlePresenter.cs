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
        [Inject] UiMenuItemModelGameStart _gameStartModel;
        [Inject] PresenterCoreFactoryTitle _presenterCoreFactoryTitle;
        [Inject] IMainLoopEntererProvider _adapter;

        [Inject] IDisposablePure _compositeDisposable;

        public void PostInitialize()
        {
            _gameStartModel.Entered.Subscribe(_ => _adapter.ProvideMainLoopAdapter().Enter().Forget()).AddTo(_compositeDisposable);
            _gameStartModel.Entered.Subscribe(_ => _presenterCoreFactoryTitle.GetGate().MenuEnd()).AddTo(_compositeDisposable
                );

        }
    }
}