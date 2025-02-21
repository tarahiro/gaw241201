using Cysharp.Threading.Tasks;
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

        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x => _view.Enter(x).Forget()).AddTo(_disposable);
            _view.Exited.Subscribe(_model.EndFlow).AddTo(_disposable);
            _uiDeletable.UiDeleted.Subscribe(_ => _view.Delete()).AddTo(_disposable);
        }
    }
}