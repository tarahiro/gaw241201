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

        public async UniTask EnterMainLoop()
        {
            Log.Comment("フローループ開始");

            //とりあえず上から読んでいく 後々はConditionを見る
            for(int i = 0; i < _masterDataProvider.Count; i++)
            {
                Log.Comment("フロー開始");
                var master = _masterDataProvider.TryGetFromIndex(i).GetMaster();
                var item = _flowProvider.GetFlowModel(master.Category);
                await item.EnterFlow(master.BodyId);
            }

        }
    }
}