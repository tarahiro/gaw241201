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
    public class HorrorStoryMainLoopStarter : IMainLoopStarter
    {
        [Inject] IFlowHundler _flowHundler;
        [Inject] IGlobalFlagProvider _globalFlagProvider;

        public void EnterMainLoop()
        {
            SoundManager.PlayBGM("Main");
            _flowHundler.Enter();

            if (_globalFlagProvider.GetFlag(FlagConst.Key.IsSaveDataExist) == "False")
            {
                _flowHundler.EnterFlowLoop(FlowMasterConst.FlowMasterLabel.MainFlow);
            }
            else
            {
                _flowHundler.EnterFlowLoop(FlowMasterConst.FlowMasterLabel.SaveDataExistFlow);
            }
        }
    }
}