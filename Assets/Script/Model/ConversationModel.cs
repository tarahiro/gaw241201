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
    public class ConversationModel : IFlowModel
    {
        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment(bodyId + "��Conversation�J�n");
        }
    }
}