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
        [Inject] DeleteUiModel _deleteUiModel;
        [Inject] ClickInputModel _clickInputModel;

        public IFlowModel GetFlowModel(FlowConst.Category category)
        {
            Log.Comment("フロー取得");

            switch (category)
            {
                case FlowConst.Category.Conversation :
                    return _conversationModel;

                case FlowConst.Category.FreeInput:
                    return _freeInputModel;

                case FlowConst.Category.RegisterFlag:
                    return _registerFlagFlowModel;

                case FlowConst.Category.DeleteUi:
                    return _deleteUiModel;

                case FlowConst.Category.ClickInput:
                    return _clickInputModel;

                default:
                    Log.DebugAssert("不正なカテゴリー名です");
                    return null;
            }
        }

    }
}