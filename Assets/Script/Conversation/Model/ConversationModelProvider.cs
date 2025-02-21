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
        [Inject] IConversationModelFactory _factory;

        public IConversationModel MainConversationModel { get; private set; }

        public IConversationModel SettingConversationModel { get; private set; }

        public ICategoryEnterableModel ConversationModel { get; private set; }
    
        public void Initialize()
        {
            var model = _factory.CreateMainModel();

            MainConversationModel = model;
            SettingConversationModel = _factory.CreateSettingModel();
            ConversationModel = model;
        }
    }
}