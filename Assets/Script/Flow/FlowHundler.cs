using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class FlowHundler : IMainLoopHundler
    {
        [Inject] IFlowMasterDataProvider _masterDataProvider;
        [Inject] IFlowProvider _flowProvider;

        IFlowModel _currentFlow;

        public async UniTask EnterMainLoop()
        {
            Log.Comment("フローループ開始");
            SoundManager.PlayBGM("Main");

            //とりあえず上から読んでいく 後々はConditionを見る
            for(int i = 0; i < _masterDataProvider.Count; i++)
            {
                Log.Comment("フロー開始");
                var master = _masterDataProvider.TryGetFromIndex(i).GetMaster();
                _currentFlow = _flowProvider.GetFlowModel(EnumUtil.KeyToType<FlowConst.Category>(master.Category));
                await _currentFlow.EnterFlow(master.BodyId);
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