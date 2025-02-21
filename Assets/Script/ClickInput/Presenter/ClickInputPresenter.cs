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
    public class ClickInputPresenter : IPostInitializable

    {
        [Inject] ClickInputModel _model;
        [Inject] ClickInputView _view;
        [Inject] DeleteClickInputUi _deleteUi;

        [Inject] SelectInputModelFake _selectInputModelFake;
        [Inject] SelectInputView _selectInputView;
        [Inject] SelectInputDisplayView _selectInputDisplayView;
        [Inject] SelectInputProcessor _selectInputProcessor;

        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x => _view.Enter(x).Forget()).AddTo(_disposable);
            _deleteUi.UiDeleted.Subscribe(x => _view.Delete()).AddTo(_disposable);
            _view.Exited.Subscribe(_model.End).AddTo(_disposable);

            _selectInputModelFake.Entered.Subscribe(_selectInputView.Enter).AddTo(_disposable);
            _selectInputProcessor.LrInputted.Subscribe(_selectInputModelFake.Focus).AddTo(_disposable);
            _selectInputModelFake.Focused.Subscribe(_selectInputDisplayView.Focus).AddTo(_disposable);
            _selectInputProcessor.Decided.Subscribe(_ => _selectInputModelFake.Exit()).AddTo(_disposable);
            _selectInputModelFake.Exited.Subscribe(_ => _selectInputView.Exit()).AddTo(_disposable);
        }
    }
}