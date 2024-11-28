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
        [Inject] FreeInputModel _freeInputModel;
        [Inject] RegisterFlagFlowModel _registerFlagFlowModel;

        public IFlowModel GetFlowModel(string category)
        {
            Log.Comment("フロー取得");

            switch (category)
            {
                case "Conversation":
                    return _conversationModel;

                case "FreeInput":
                    return _freeInputModel;

                case "RegisterFlag":
                    return _registerFlagFlowModel;

                default:
                    Log.DebugAssert("不正なカテゴリー名です");
                    return null;
            }
        }

    }
}