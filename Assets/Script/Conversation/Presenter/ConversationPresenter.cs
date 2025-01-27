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
        [Inject] ISingleTextSequenceEnterable<IConversationMaster> _enterable;


        IConversationView _mainConversationView;
      //  ConversationView _settingConversationView;

        [Inject] IConversationViewFactory _conversationViewFactory;

        [Inject] ConversationViewArgsFactory _viewArgsFactory;

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _mainConversationView = _conversationViewFactory.CreateSettingConversation();

            _enterable.Entered.Subscribe(x =>  _mainConversationView.EnterConversation(_viewArgsFactory.Create(x)).Forget()).AddTo(_disposable);
            _mainConversationView.Completed.Subscribe(x => _model.EndSingle()).AddTo(_disposable);
        }
    }
}