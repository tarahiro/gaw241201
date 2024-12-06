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
    public class MonitorPresenter : IPostInitializable
    {
        [Inject] StartMonitorModel _startModel;
        [Inject] MonitorView _view;
        [Inject] HaltModel _haltModel;

        CompositeDisposable _disposable = new CompositeDisposable();
        public void PostInitialize()
        {
            _startModel.Entered.Subscribe(ct => _view.Monitor(ct).Forget()).AddTo(_disposable);
            _view.ForceEnded.Subscribe(_ => _haltModel.Halt()).AddTo(_disposable);
        }
    }
}