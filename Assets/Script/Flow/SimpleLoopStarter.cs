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
    public class SimpleLoopStarter : IMainLoopStarter
    {
        [Inject] IFlowHundler _flowHundler;
        [Inject] FlowMasterConst.FlowMasterLabel _flowMasterLabel;
        [Inject] ILoopInitializer _loopInitializer;
        public void EnterMainLoop()
        {
            _loopInitializer.InitializeLoop();
            _flowHundler.EnterFlowLoop(_flowMasterLabel);
        }
    }
}