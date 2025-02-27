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
        [Inject] SkillEnterView _rootView;
        [Inject] SkillAchieveArgsDataFactory _factory;
        [Inject] SkillInputProcessor _inputProcessor;

        [Inject] SkillMenuView _menuView;

        [Inject] SkillMenuModel _menuModel;
        [Inject] SkillIndexInputView _inputView;
        [Inject] IndexVariantHundlerSkill _indexVariantHundler;

        [Inject] SkillMenuItemProvider _provider;

        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {

            for(int i = 0; i < _provider.Count; i++)
            {
                _provider.ProvideRaw(i).Setted.Subscribe(_menuView.SetData).AddTo(_disposable);
                _provider.ProvideRaw(i).Entered.Subscribe(_model.End).AddTo(_disposable);
            }

            _model.OnNumberDecided.Subscribe(_indexVariantHundler.SetMaxNumber).AddTo(_disposable);

            _factory.Initialize();
        }
    }
}