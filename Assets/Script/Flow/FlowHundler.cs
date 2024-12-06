using Cysharp.Threading.Tasks;
using gaw241201.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class FlowHundler : IMainLoopHundler
    {
        [Inject] IFlowMasterDataDictionaryProvider _masterDataDictionaryProvider;
        [Inject] IFlowProvider _flowProvider;
        [Inject] IGlobalFlagProvider _globalFlagProvider;

        IFlowModel _currentFlow;

        CancellationTokenSource _cancellationTokenSource;

        public void EnterMainLoop()
        {
            SoundManager.PlayBGM("Main");

            if (_globalFlagProvider.GetFlag(FlagConst.Key.IsSaveDataExist) == "False")
            {
                EnterFlowLoop(FlowConst.FlowName.MainFlow);
            }
            else
            {
                EnterFlowLoop(FlowConst.FlowName.SaveDataExistFlow);
            }
        }

        public void SwitchFlow(FlowConst.FlowName nextFlowName, string specificId = "")
        {
            _cancellationTokenSource.Cancel();

            //本当は_cancellationTokenにRegisterしたかったが、
            if(_currentFlow != null)_currentFlow.ForceEndFlow();

            EnterFlowLoop(nextFlowName,specificId);
        }

        void EnterFlowLoop(FlowConst.FlowName flowName, string specificId = "")
        {
            _cancellationTokenSource = new CancellationTokenSource();
            FlowLoop(flowName, _cancellationTokenSource.Token, specificId).Forget();
        }

        async UniTask FlowLoop(FlowConst.FlowName flowName, CancellationToken cancellationToken, string specificId)
        {
            Log.Comment("フローループ開始");

            var _provider = _masterDataDictionaryProvider.GetProvider(flowName.ToString());


            int firstId = 0;

            if (specificId != "")
            {
                firstId = _provider.TryGetFromId(specificId).Index;
            }

            //とりあえず上から読んでいく 後々はConditionを見る
            for (int i = firstId; i < _provider.Count && !cancellationToken.IsCancellationRequested; i++)
            {
                Log.Comment("フロー開始");
                var master = _provider.TryGetFromIndex(i).GetMaster();
                _currentFlow = _flowProvider.GetFlowModel(EnumUtil.KeyToType<FlowConst.Category>(master.Category));
                await _currentFlow.EnterFlow(master.BodyId);
                _currentFlow = null;
            }

            //Fake
            if(flowName == FlowConst.FlowName.SaveDataExistFlow)
            {
                SwitchFlow(FlowConst.FlowName.MainFlow, "207000TypingConversation");
            }
        }


#if ENABLE_DEBUG
        public IFlowModel ForceGetCurrentFlow()
        {
            return _currentFlow;
        }
#endif
    }
}