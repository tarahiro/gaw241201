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
        [Inject] IFreeInputSwithcerModel _switcherModel;

        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {
            foreach(var item in _switcherModel._freeInputGateModelList())
            {
                item.Exited.Subscribe(_ => _model.EndFlow()).AddTo(_disposable);
            }
        }
    }
}