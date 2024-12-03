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
    public class ConfiscatePresenter : IPostInitializable
    {
        [Inject] ConfiscateModel _model;
        [Inject] ConfiscateView _view;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(_view.Confiscate).AddTo(_disposable);
        }

    }
}