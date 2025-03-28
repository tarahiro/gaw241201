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
    public class DrumRollPresenterFactory
    {
        DrumRollModel _model;
        DrumRollGateView _gateView;
        DrumRollInputProcessor _inputProcessor;

        DrumRollInputView _inputView;

        [Inject] DrumRollDisplayView _displayView;

        [Inject] IInputViewFactory _inputViewFactory;
        [Inject] InputExecutorCommand _executor;
        [Inject] InputExecutorDiscreteDirectionHorizontal _executorHorizontal;
        [Inject] IDisposablePure _disposable;
        [Inject] ICancellationTokenPure _token;


        public void Create()
        {
            _model = new DrumRollModel();
            _inputProcessor = new DrumRollInputProcessor(_executor,_executorHorizontal,_disposable);
            _inputView = new DrumRollInputView(_inputViewFactory, _inputProcessor);
            _gateView = new DrumRollGateView(_displayView, _inputView, _token);

            var presenter = new DrumRollPresenter(_model, _gateView, _displayView, _inputProcessor, _disposable);
            presenter.Present();
        }

        public DrumRollModel GetDrumRollModel()
        {
            return _model;
        }
    }
}