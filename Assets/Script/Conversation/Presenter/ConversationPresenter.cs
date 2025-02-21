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
        [Inject] ConversationModelProvider _modelProvider;

        IConversationView _mainConversationView;
        IConversationView _settingConversationView;

        [Inject] IConversationViewFactory _conversationViewFactory;
        [Inject] ConversationViewArgsFactory _viewArgsFactory;
        [Inject] IDisposablePure _disposable;

        public void PostInitialize()
        {
            _modelProvider.Initialize();
            _modelProvider.MainConversationModel.Initialize((x => _mainConversationView.EnterConversation(_viewArgsFactory.Create(x))), _disposable);
            _modelProvider.SettingConversationModel.Initialize((x => _settingConversationView.EnterConversation(_viewArgsFactory.Create(x))), _disposable);

            _mainConversationView = _conversationViewFactory.CreateMainConversation();
            _settingConversationView = _conversationViewFactory.CreateSettingConversation();

            _mainConversationView.Completed.Subscribe(x => _modelProvider.MainConversationModel.EndSingle()).AddTo(_disposable);
            _settingConversationView.Completed.Subscribe(x => _modelProvider.SettingConversationModel.EndSingle()).AddTo(_disposable);
        }
    }
}