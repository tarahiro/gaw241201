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
    public class SkillPresenter : IPostInitializable
    {
        [Inject] SkillAchieveModel _model;
        [Inject] SkillEnterView _view;
        [Inject] SkillAchieveArgsDataFactory _factory;
        [Inject] SkillMenuView _menuView;

        [Inject] SkillMenuModel _menuModel;
        [Inject] SkillIndexInputView _inputView;

        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {
            _menuModel.Entered.Subscribe(x => _view.Enter(x).Forget()).AddTo(_disposable);
            _menuModel.DecidedSkill.Subscribe(_model.End).AddTo(_disposable);

            _inputView.IndexerMoved.Subscribe(_menuModel.MoveFocus).AddTo(_disposable);
            _inputView.Decided.Subscribe(_ => _menuModel.Decide()).AddTo(_disposable);
            _menuModel.FocusChanged.Subscribe(x =>
            {
                _inputView.ChangeFocus(x);
                _menuView.ChangeFocus(x);
            }).AddTo(_disposable);

            _factory.Initialize();
            _menuModel.Initialize();
            _model.Exited.Subscribe(x => _view.Exit().Forget()).AddTo(_disposable);
        }
    }
}