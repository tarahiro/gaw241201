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
    public class CommandProcessorFactory : ICommandProcessorFactory
    {
        [Inject] FlowSwitchPublisher _publisher;
        [Inject] ConversationModelProvider _conversationModel;

        public ISwitchCommandProcessor Create(SwitchConst.TargetCategory targetCategory)
        {
            switch (targetCategory)
            {
                case SwitchConst.TargetCategory.Flow:
                    return new CommandProcessorFlow(_publisher);
                    
                    case SwitchConst.TargetCategory.Conversation:
                    return new CommandProcessorConversation(_conversationModel);

                default:
                    Log.DebugAssert(targetCategory + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                    return null;
            }
        }
    }
}