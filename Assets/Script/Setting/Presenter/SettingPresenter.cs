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


    public class SettingPresenter : IPostInitializable
    {
        [Inject] SettingStarter _starter;
        [Inject] SettingExiter _exiter;

        [Inject] SettingView _view;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _starter.SettingStarted.Subscribe(x => _view.Enter(x).Forget()).AddTo(_disposable);
            _exiter.SettingStarted.Subscribe(x => _view.Exit(x).Forget()).AddTo(_disposable);
        }
    }
}