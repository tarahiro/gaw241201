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
    public class ActStartPresenter : IPostInitializable
    {
        [Inject] ActStartModel _model;
        [Inject] ActStartView _view;
        [Inject] ActViewArgsListFactory _factory;

        CompositeDisposable disposables = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x => _view.Enter(_factory.Create(x))).AddTo(disposables);
        }
    }
}