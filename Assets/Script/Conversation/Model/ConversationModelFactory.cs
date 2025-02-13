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
    public class ConversationModelFactory
    {
        [Inject] IGroupMasterGettable<IConversationMaster> _groupMasterGettable;
        [Inject] ModelArgsFactory<IConversationMaster> _modelArgsFactory;
        public ConversationModel CreateMainModel()
        {
            return new ConversationModel(new SingleTextSequenceEnterer<IConversationMaster>(_modelArgsFactory), _groupMasterGettable);
        }

        public ConversationModel CreateSettingModel()
        {
            return new ConversationModel(new SingleTextSequenceEnterer<IConversationMaster>(_modelArgsFactory), _groupMasterGettable);
        }
    }
}