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
    public class TitlePresenter
    {
        [Inject] UiMenuItemModelGameStart _gameStartModel;
        [Inject] UiMenuItemModelLanguage _languageModel;
        [Inject] PresenterCoreFactoryTitle _presenterCoreFactoryTitle;

        [Inject] DrumRollPresenterFactory _drumRollPresenterFactory;
        [Inject] IMainLoopEntererProvider _adapter;

        [Inject] IDisposablePure _compositeDisposable;

        public void Present()
        {
            _gameStartModel.Entered.Subscribe(_ => _adapter.ProvideMainLoopAdapter().Enter().Forget()).AddTo(_compositeDisposable);
            _gameStartModel.Entered.Subscribe(_ => _presenterCoreFactoryTitle.GetGate().MenuEnd()).AddTo(_compositeDisposable
                );

            _languageModel.Entered.Subscribe(_ => _drumRollPresenterFactory.GetDrumRollModel().Enter()).AddTo(_compositeDisposable);
            _languageModel.Exited.Subscribe(_ => _drumRollPresenterFactory.GetDrumRollModel().Exit()).AddTo(_compositeDisposable);
            _drumRollPresenterFactory.GetDrumRollModel().Decided.Subscribe(_languageModel.Decide).AddTo(_compositeDisposable);
        }
    }
}