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
        [Inject] SkillInputProcessor _inputProcessor;

        [Inject] SkillMenuView _menuView;

        [Inject] SkillMenuModel _menuModel;
        [Inject] SkillIndexInputView _inputView;
        [Inject] IndexVariantHundlerSkill _indexVariantHundler;

        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {

            _inputProcessor.IndexerMoved.Subscribe(_menuModel.MoveFocus).AddTo(_disposable);
            _inputProcessor.Decided.Subscribe(_ => _menuModel.Decide()).AddTo(_disposable);

            _menuModel.Entered.Subscribe(_ => _view.EnterRoot()).AddTo(_disposable);
            _menuModel.DecidedSkill.Subscribe(_model.End).AddTo(_disposable);
            _menuModel.FocusChanged.Subscribe(x => _menuView.SetFocus(x).Forget()).AddTo(_disposable);
            _menuModel.Exited.Subscribe(_ => _menuView.Exit().Forget()).AddTo(_disposable);

            _model.Exited.Subscribe(x => _view.EndRoot()).AddTo(_disposable);

            _menuModel.ArgsSetted.Subscribe(x =>
            {
                _menuView.SetData(x.DataList);
                _indexVariantHundler.SetMaxNumber(x.DataList.Count);
            }).AddTo(_disposable);

            _factory.Initialize();
        }
    }
}