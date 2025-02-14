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

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x => _view.Enter(x).Forget()).AddTo(_compositeDisposable);
            _deleteUi.UiDeleted.Subscribe(x => _view.Delete()).AddTo(_compositeDisposable);
            _view.Exited.Subscribe(_model.End).AddTo(_compositeDisposable);

            _selectInputModelFake.Entered.Subscribe(_selectInputView.Enter).AddTo(_compositeDisposable);
            _selectInputProcessor.LrInputted.Subscribe(_selectInputModelFake.Focus).AddTo(_compositeDisposable);
            _selectInputModelFake.Focused.Subscribe(_selectInputDisplayView.Focus).AddTo(_compositeDisposable);
            _selectInputProcessor.Decided.Subscribe(_ => _selectInputModelFake.Exit()).AddTo(_compositeDisposable);
            _selectInputModelFake.Exited.Subscribe(_ => _selectInputView.Exit()).AddTo(_compositeDisposable);
        }
    }
}