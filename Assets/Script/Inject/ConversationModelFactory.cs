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
    public class ConversationModelFactory : IConversationModelFactory
    {
        [Inject] IGroupMasterGettable<IConversationMaster> _groupMasterGettable;
        [Inject] ModelArgsFactory<IConversationMaster> _modelArgsFactory;
        [Inject] IObjectResolver _diContainer;

        public ConversationModel CreateMainModel()
        {
            return new ConversationModel(new SingleTextSequenceEnterer<IConversationMaster>(_modelArgsFactory), _groupMasterGettable, _diContainer.Resolve<ICancellationTokenPure>());
        }

        public ConversationModel CreateSettingModel()
        {
            return new ConversationModel(new SingleTextSequenceEnterer<IConversationMaster>(_modelArgsFactory), _groupMasterGettable, _diContainer.Resolve<ICancellationTokenPure>());
        }
    }
}