using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class FreeInputPresenter : IPostInitializable

    {
        [Inject] FreeInputModel _model;
        [Inject] FreeInputView _view;
        [Inject] IUiDeletable _uiDeletable;
        [Inject] IUiViewDeletable _uiViewDeletable;

        CompositeDisposable _disposable = new CompositeDisposable();
        CancellationTokenSource _cts = new CancellationTokenSource();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x => _view.Enter(x,_cts.Token).Forget()).AddTo(_disposable);
            _view.Exited.Subscribe(_model.EndFlow).AddTo(_disposable);
            _uiDeletable.UiDeleted.Subscribe(_ => _view.Delete()).AddTo(_disposable);
        }
    }
}