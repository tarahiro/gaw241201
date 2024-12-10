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
    public class TypingPresenter : IPostInitializable
    {
        [Inject] TypingModel _model;
        [Inject] TypingView _view;
        [Inject] TypingViewArgsFactory _argsFactory;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x => _view.Enter(_argsFactory.Create(x)).Forget()).AddTo(_disposable);
            _view.Exited.Subscribe(_ => _model.EndSingle()).AddTo(_disposable);

            _view.Initialize();
        }
    }
}