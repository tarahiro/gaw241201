using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using gaw241201.Model;
using gaw241201.View;

namespace gaw241201.Inject
{
#if ENABLE_DEBUG
    public class GlobalDebugManager : ITickable, IStartable
    {
        [Inject] FlowHundler _flowHundler;
        [Inject] ISaveDeletable _saveDeletable;
        [Inject] LanguageModel _languageModel;
        [Inject] EnterEffectModel _effectModel;
        [Inject] ConversationModelProvider _conversationModelProvider;
        [Inject] FacialMessagePublisher _facialMessagePublisher;
        [Inject] ResetGazeMessagePublisher _resetGazeMessagePublisher;

        public void Start()
        {

        }

        public void Tick()
        {

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    _flowHundler.ForceGetCurrentFlow().ForceEndFlow();
                }


                if (Input.GetKeyDown(KeyCode.Q))
                {
                    _saveDeletable.DeleteSaveData();
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    _languageModel.SetLanguage(EnumUtil.NoToType<LanguageConst.AvailableLanguage>(((int)_languageModel.Language + 1) % LanguageConst.AvailableLanguageNumber));
                }

                if (Input.GetKeyDown(KeyCode.E)) {
                    _effectModel.EnterFlow("GlitchLargeAutoEnd").Forget();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    _effectModel.EnterFlow("GlitchSmall").Forget();
                }

                if (Input.GetKeyDown(KeyCode.T))
                {
                    Log.DebugLog("デバッグ開始");
                    _conversationModelProvider.SettingConversationModel.EnterFlow("940500GoatEyeConversation").Forget();
                }

                if (Input.GetKeyDown(KeyCode.Y))
                {
                    _facialMessagePublisher.PublishEvent(GazeConst.GazingKey.Main, ConversationViewConst.Facial.Mad);
                    _facialMessagePublisher.PublishEvent(GazeConst.GazingKey.Main, ConversationViewConst.Facial.Big);
                    _resetGazeMessagePublisher.PublishEvent(GazeConst.GazingKey.Main, Unit.Default);
                    _resetGazeMessagePublisher.PublishEvent(GazeConst.GazingKey.Card, Unit.Default);
                }

            }
        }
    }
#endif
}