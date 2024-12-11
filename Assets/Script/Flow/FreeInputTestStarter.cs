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
#if ENABLE_DEBUG
    public class FreeInputTestStarter : IMainLoopStarter
    {
        [Inject] IFlowHundler _flowHundler;

        public void EnterMainLoop()
        {
            SoundManager.PlayBGM("Main");
            _flowHundler.EnterFlowLoop(FlowMasterConst.FlowMasterLabel.FreeInputTestFlow);
        }
    }
#endif
}