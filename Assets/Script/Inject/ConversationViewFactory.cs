using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class ConversationViewFactory : IConversationViewFactory
    {
        [Inject] ConversationTextViewProvider _conversationTextViewProvider;
        [Inject] MainEyesView _mainEyesView;
        [Inject] ImpressionView _impressionView;

        [Inject] SettingEyesView _settingEyesView;

        public IConversationView CreateMainConversation()
        {
            return new ConversationView(_conversationTextViewProvider.GetMainView(), _mainEyesView, _mainEyesView, _impressionView);
        }
        
        public IConversationView CreateSettingConversation()
        {
            return new ConversationView(_conversationTextViewProvider.GetSettingView(), _settingEyesView, _settingEyesView, _impressionView);
        }
        
    }
}