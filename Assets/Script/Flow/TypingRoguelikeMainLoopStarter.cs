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
    public class TypingRoguelikeMainLoopStarter : IMainLoopStarter
    {
        [Inject] IFlowHundler _flowHundler;

        public void EnterMainLoop()
        {
            Log.Comment("TypingRogueLikeのメインループ開始");

            SoundManager.PlayBGM("Main");
            _flowHundler.EnterFlowLoop(FlowMasterConst.FlowMasterLabel.TypingRoguelikeMainFlow);
        }
    }
}