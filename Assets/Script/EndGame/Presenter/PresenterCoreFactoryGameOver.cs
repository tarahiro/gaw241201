using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201
{
    public class PresenterCoreFactoryGameOver :  IEndGameUiGateModelFactory
    {

        //UiPresenterに必要なもの
        IMenuModelGate _gateModel;
        IUiMenuModel _menuModel;
        [Inject] UiMenuEnterView _enterView;
        UiMenuInputProcessor _inputProcessor;
        [Inject] MenuView _menuView;
        [Inject] IDisposablePure _disposable;

        //間接的に必要なもの
        IInputView _inputView;
        [Inject] IInputViewFactory _inputViewFactory;
        [Inject] IUiMenuInputProcessorFactory _inputProcessorFactory;

        [Inject] UiMenuModelFactory _menuModelFactory;
        [Inject] MenuItemRestartProvider _menuItemRestartProvider;

        public void Create()
        {
            _menuModel = _menuModelFactory.Create(_menuItemRestartProvider);
            _gateModel = new MenuRootModelRestart(new MenuModelGate(_menuModel));

            _inputProcessor = _inputProcessorFactory.Create(_menuView); 
            _inputView = new UiMenuInputView(_inputViewFactory, _inputProcessor);
            _enterView.Construct(_inputView);


            var presenter = new UiPresenterCore(
                _gateModel,
                _gateModel,
                _enterView,
                _inputProcessor,
                _menuModel,
                new List<IUiMenuModel>() { _menuModel },
                _menuView,
                _disposable);
            presenter.Present();

        }

        public IMenuModelGate GetGateModel()
        {
            return _gateModel;
        }
    }
}