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
    public class PresenterCoreFactoryTemplate : IPostInitializable
    {
        [Inject] MenuRootModelTemplate _achieveModel;
        [Inject] UiEnterViewTemplate _rootView;
        [Inject] UiInputProcessorTemplate _inputProcessor;
        [Inject] UiMenuModelTemplate _menuModel;
        [Inject] UiMenuViewTemplate _menuView;
        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {
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
    }
}