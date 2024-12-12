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
        [Inject] LifetimeScope[] _scope;

        [Inject] ConversationModel _conversationModel;
        [Inject] FreeInputModel _freeInputModel;
        [Inject] RegisterFlagFlowModel _registerFlagFlowModel;
        [Inject] DeleteUiModel _deleteUiModel;
        [Inject] ClickInputModel _clickInputModel;
        [Inject] TypingModel _typingModel;
        [Inject] ConfiscateModel _confiscateModel;
        [Inject] EnterEffectModel _enterEffectModel;
        [Inject] EndEffectModel _endEffectModel;
        [Inject] EndGameModel _endGameModel;
        [Inject] StartMonitorModel _startMonitorModel;
     //   [Inject] TypingRoguelikeModel _typingRoguelikeModel;

        public IFlowModel GetFlowModel(FlowConst.Category category)
        {
            Log.Comment("フロー取得");

            switch (category)
            {
                case FlowConst.Category.Conversation:
                    return _conversationModel;

                case FlowConst.Category.FreeInput:
                    return _freeInputModel;

                case FlowConst.Category.RegisterFlag:
                    return _registerFlagFlowModel;

                case FlowConst.Category.DeleteUi:
                    return _deleteUiModel;

                case FlowConst.Category.ClickInput:
                    return _clickInputModel;

                case FlowConst.Category.Typing:
                    return _typingModel;

                case FlowConst.Category.Confiscate:
                    return _confiscateModel;

                case FlowConst.Category.EnterEffect:
                    return _enterEffectModel;

                case FlowConst.Category.EndEffect:
                    return _endEffectModel;

                case FlowConst.Category.EndGame:
                    return _endGameModel;

                case FlowConst.Category.StartMonitor:
                    return _startMonitorModel;

                case FlowConst.Category.TypingRoguelike:
                    return InjectUtil.GetInstance<TypingRoguelikeModel>(_scope);

                default:
                    Log.DebugAssert("不正なカテゴリー名です");
                    return null;
            }
        }

    }
}