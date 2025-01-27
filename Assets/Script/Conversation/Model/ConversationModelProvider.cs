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
    public class ConversationModelProvider
    {
        [Inject] ConversationModelFactory _factory;

        public IConversationModel MainConversationModel { get; private set; }

        public IConversationModel SettingConversationModel { get; private set; }
    
        public void Initialize()
        {
            MainConversationModel = _factory.CreateMainModel();
            SettingConversationModel = _factory.CreateSettingModel();
        }
    }
}