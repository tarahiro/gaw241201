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
    public class EndGamePresenter : IPostInitializable, IDisposable
    {
        [Inject] EndGameModel _model;
        [Inject] EndGameView _view;

        [Inject] GameOverExhibitionInputProcessor _processor;

        CompositeDisposable disposables = new CompositeDisposable();
        public void PostInitialize()
        {
            _model.Entered.Subscribe(_view.Enter).AddTo(disposables);
            _view.Clicked.Subscribe(_ => _model.Exit()).AddTo(disposables);

            _processor.Decided.Subscribe(_ => _model.Exit()).AddTo(disposables);
            _processor.Canceled.Subscribe(_ => _model.Exit()).AddTo(disposables);
        }

        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}