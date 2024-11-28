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
    public class FlowProvider : IFlowProvider
    {
        [Inject] ConversationModel _conversationModel;

        public IFlowModel GetFlowModel(string category)
        {
            Log.Comment("�t���[�擾");

            switch (category)
            {
                case "Conversation":
                    return _conversationModel;

                default:
                    Log.DebugAssert("�s���ȃJ�e�S���[���ł�");
                    return null;
            }
        }

    }
}