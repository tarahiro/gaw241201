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
    public class MainLoopStarter : IAdapterManagerToModel
    {
        [Inject] IFlowHundler _flowHundler;
        [Inject] IGlobalFlagProvider _globalFlagProvider;
        [Inject] ILoopInitializer _loopInitializer;

        public async UniTask Enter()
        {
            _loopInitializer.InitializeLoop();
            _flowHundler.Enter();

            /*
            if (_globalFlagProvider.GetFlag(FlagConst.Key.IsSaveDataExist) == Tarahiro.Const.c_false)
            {
                _flowHundler.EnterFlowLoop(FlowMasterConst.FlowMasterLabel.HorrorStoryMainFlow);
            }
            else
            {
                _flowHundler.EnterFlowLoop(FlowMasterConst.FlowMasterLabel.SaveDataExistFlow);
            }
            */

            if (_globalFlagProvider.IsContainskey(FlagConst.Key.RestartFlow))
            {
                _flowHundler.EnterFlowLoop(EnumUtil.KeyToType<FlowMasterConst.FlowMasterLabel>(_globalFlagProvider.GetFlag(FlagConst.Key.RestartFlow)));

            }
            else
            {
                _flowHundler.EnterFlowLoop(FlowMasterConst.FlowMasterLabel.HorrorStoryMainFlow);
            }

        }
    }
}