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
    public class Switcher : ICategoryEnterableModel
    {
        [Inject] ISwitchMasterDataProvider _masterDataProvider;

        [Inject] IByStringGetterFactory _byStringGetterFactory;
        [Inject] IConditionJudgerFactory _conditionJudgerFactory;
        [Inject] ICommandProcessorFactory _commandProcessorFactory;

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "開始");
            var master = _masterDataProvider.TryGetFromId(bodyId).GetMaster();

            Log.Comment("判断に使うValueを取得");
            string value = _byStringGetterFactory.
                Create(master.ByCategory).
                ByStringGet(master.ByKey);

            List<ISwitchConditionJudger> _judgerList = new List<ISwitchConditionJudger>();
            for (int i = 0; i < master.ConditionAndValueList.Count; i++)
            {
                _judgerList.
                    Add(_conditionJudgerFactory.Create(master.ConditionAndValueList[i].ConditionId));
            }

            for (int i = 0; i < _judgerList.Count; i++)
            {
                Log.Comment("//Conditionごとに判断");
                if (_judgerList[i].IsMatch(master.ConditionAndValueList[i].ConditionId, value))
                {
                    Log.Comment("//整合するConditionがあれば、そのセットのValueと、TargetCategoryに合わせて、処理");
                    await _commandProcessorFactory.
                        Create(master.TargetCategory).
                        Process(master.ConditionAndValueList[i].Value);
                    break;
                }

            }
            Log.Comment("整合するConditionがなかったら、終了");

            _judgerList = new List<ISwitchConditionJudger>();

        }
        public void ForceEndFlow()
        {
        }

    }
}