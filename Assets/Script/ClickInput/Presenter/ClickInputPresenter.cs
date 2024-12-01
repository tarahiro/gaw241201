using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.Model;
using gaw241201.View;


namespace gaw241201.Presenter
{
    public class ClickInputPresenter : IPostInitializable

    {
        [Inject] ClickInputModel _model;
        [Inject] ClickInputView _view;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x => _view.Enter(x).Forget()).AddTo(_compositeDisposable);
            _view.Exited.Subscribe(_ => _model.End()).AddTo(_compositeDisposable);
        }
    }
}