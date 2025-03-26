using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class PresenterCoreFactoryTitle
    {

        //UiPresenterに必要なもの
        IMenuModelGate _gateModel => _menuRootModelTitle;
        IUiMenuModel _menuModel;
        [Inject] UiMenuEnterView _enterView;
        UiMenuInputProcessor _inputProcessor;
        [Inject] MenuView _menuView;
        [Inject] IDisposablePure _disposable;

        //間接的に必要なもの
        IInputView _inputView;
        [Inject] IInputViewFactory _inputViewFactory;
        [Inject] IUiMenuInputProcessorFactory _inputProcessorFactory;

        MenuRootModelTitle _menuRootModelTitle;
        [Inject] UiMenuModelFactory _menuModelFactory;
        [Inject] MenuItemTitleProvider _menuItemTitleProvider;
        public void Create()
        {
            _menuModel = _menuModelFactory.Create(_menuItemTitleProvider);
            _menuRootModelTitle = new MenuRootModelTitle(new MenuModelGate(_menuModel));

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

        public IAdapterManagerToModel GetAdapter()
        {
            return _menuRootModelTitle;
        }

        public IMenuModelGate GetGate()
        {
            return _gateModel;
        }

    }
}