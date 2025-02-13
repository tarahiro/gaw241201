using Cysharp.Threading.Tasks;
using MessagePipe;
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
    public class SettingEventCatcher
    {
        [Inject] IGlobalFlagProvider _globalFlagProvider;
        [Inject] IGlobalFlagRegisterer _globalFlagRegisterer;

        [Inject] ConversationModelProvider _conversationModelProvider;

        public void OnEnter()
        {
            string value = _globalFlagProvider.GetFlag(FlagConst.Key.OnEnterSettingConversation);
            if (value != "")
            {
                _conversationModelProvider.SettingConversationModel.Enter(value);
                _globalFlagRegisterer.RegisterFlag(FlagConst.Key.OnEnterSettingConversation, "");
            }
        }
    }
}