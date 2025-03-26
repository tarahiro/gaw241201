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


namespace gaw241201.Presenter
{
    public class UiPresenterCoreSkillFactory : IPostInitializable
    {
        [Inject] SkillAchieveModel _achieveModel;
        [Inject] SkillEnterView _rootView;
        [Inject] SkillInputProcessor _inputProcessor;
        [Inject] SkillMenuModel _menuModel;
        [Inject] SkillMenuView _menuView;
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
            presenter.Present();

        }
    }
}