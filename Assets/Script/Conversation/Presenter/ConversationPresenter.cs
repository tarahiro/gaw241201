using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class ConversationPresenter : IPostInitializable
    {
        [Inject] ConversationModel _model;
        [Inject] ConversationView _view;
        [Inject] ConversationViewArgsFactory _viewArgsFactory;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x =>  _view.EnterConversation(_viewArgsFactory.Create(x)).Forget()).AddTo(_disposable);
            _view.Completed.Subscribe(x => _model.EndSingle()).AddTo(_disposable);
        }
    }
}