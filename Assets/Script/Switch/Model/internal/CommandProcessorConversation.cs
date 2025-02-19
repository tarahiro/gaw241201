using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class CommandProcessorConversation : ISwitchCommandProcessor
    {
        ConversationModelProvider _conversationModelProvider;

        public CommandProcessorConversation(ConversationModelProvider conversationModel)
        {
            _conversationModelProvider = conversationModel;
        }

        public async UniTask Process(string value)
        {
            await _conversationModelProvider.ConversationModel.EnterFlow(value);
        }

    }
}