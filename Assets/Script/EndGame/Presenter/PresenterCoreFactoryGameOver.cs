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
    public class PresenterCoreFactoryGameOver :  IEndGameUiGateModelProvider
    {
        IMenuModelGate _achieveModel;
        IUiMenuModel _menuModel;
        [Inject] UiMenuEnterView _rootView;
        UiMenuInputProcessor _inputProcessor;
        [Inject] MenuView _menuView;
        [Inject] IDisposablePure _disposable;


        [Inject] InputExecutorCommand _decide;
        [Inject] InputExecutorDiscreteDirectionVertical _vertical;

        public void Create()
        {
            _menuModel = new UiMenuModelRestart(new MenuItemRestartProvider());
            _achieveModel = new MenuRootModelRestart(new MenuModelGate(_menuModel));

            _inputProcessor = new UiMenuInputProcessor(new IndexVariantHundlerUiMenu(_menuView), _decide, _vertical, _disposable);


            var presenter = new UiPresenterCore(
                _achieveModel,
                _achieveModel,
                _rootView,
                _inputProcessor,
                _menuModel,
                new List<IUiMenuModel>() { _menuModel },
                _menuView,
                _disposable);
            presenter.PostInitialize();

        }

        public IMenuModelGate GetGateModel()
        {
            return _achieveModel;
        }
    }
}