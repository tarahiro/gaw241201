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
        [Inject] SkillAchieveView _view;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x => _view.Enter(x).Forget()).AddTo(_disposable);
            _view.Ended.Subscribe(_model.End).AddTo(_disposable);
        }
    }
}