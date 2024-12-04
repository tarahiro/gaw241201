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
    public class EffectPresenter : IPostInitializable

    {
        [Inject] EnterEffectModel _enterModel;
        [Inject] EndEffectModel _endModel;
        [Inject] IEffectViewEnterable _enterView;
        [Inject] IEffectViewEndable _endView;

        CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _enterModel.Entered.Subscribe(x =>  _enterView.Enter(x).Forget()).AddTo(_compositeDisposable);
            _enterView.EnterExited.Subscribe(_ => _enterModel.End()).AddTo(_compositeDisposable);

            _endModel.Entered.Subscribe(x => _endView.End(x).Forget()).AddTo(_compositeDisposable);
            _endView.EndExited.Subscribe(_ => _endModel.End()).AddTo(_compositeDisposable);
        }
    }
}