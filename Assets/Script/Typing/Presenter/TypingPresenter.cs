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
using gaw241201.Model;


namespace gaw241201.Presenter
{
    public class TypingPresenter : IPostInitializable
    {
        [Inject] TypingModel _model;
        [Inject] ITypingView _view;
        [Inject] ISingleTextSequenceEnterable<ITypingMaster> _enterable;
        [Inject] TypingViewArgsFactory _argsFactory;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _enterable.Entered.Subscribe(x => _view.Enter(_argsFactory.Create(x)).Forget()).AddTo(_disposable);
            _view.Exited.Subscribe(_ => _model.EndSingle()).AddTo(_disposable);
        }
    }
}