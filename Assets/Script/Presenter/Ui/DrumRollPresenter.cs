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
    public class DrumRollPresenter
    {

        DrumRollModel _model;
        DrumRollGateView _gateView;
        DrumRollDisplayView _displayView;
        DrumRollInputProcessor _inputProcessor;
        IDisposablePure _disposable;

        public DrumRollPresenter(DrumRollModel model, DrumRollGateView gateView, DrumRollDisplayView displayView, DrumRollInputProcessor inputProcessor, IDisposablePure disposable)
        {
            _model = model;
            _gateView = gateView;
            _displayView = displayView;
            _inputProcessor = inputProcessor;
            _disposable = disposable;
        }

        public void Present()
        {
            _model.Entered.Subscribe(_ => _gateView.Enter().Forget()).AddTo(_disposable);
            _model.IndexChanged.Subscribe(_displayView.SetFocus).AddTo(_disposable);
            _model.Exited.Subscribe(_ => _gateView.Exit().Forget()).AddTo(_disposable);

            _model.ContentsInitialized.Subscribe(_displayView.Initialize).AddTo(_disposable);
            _inputProcessor.IndexerMoved.Subscribe(_model.ChangeFocus).AddTo(_disposable);
            _inputProcessor.Decided.Subscribe(_ => _model.Decide()).AddTo(_disposable);

            //fake
            _model.Initialize(0, new List<string> { "Jp", "En" });
        }

    }
}