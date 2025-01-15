using Cysharp.Threading.Tasks;
using gaw241201.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using Tarahiro.MasterData;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class TypingRoguelikeGroupMasterGetter : IGroupMasterGettable<ITypingRoguelikeSingleSequenceMaster>, IStageMasterRegisterable
    {
        [Inject] IMasterDataProvider<IMasterDataRecord<ITypingMaster>> _typingProvider;
        [Inject] IMasterDataProvider<IMasterDataRecord<ITypingRoguelikeMaster>> _typingRoguelikeProvider;
        [Inject] ITypingRoguelikeSingleSequenceMasterFactory _factory;

        [Inject] TypingRoguelikeConditionProvider _conditionProvider;

        Dictionary<string, List<char>> _restrictedCharDictionary = new Dictionary<string, List<char>>();
        public List<ITypingRoguelikeSingleSequenceMaster> GetGroupMaster(string bodyId)
        {
            Log.Comment("TRlSingleSequenceMaster生成開始");
            ITypingRoguelikeMaster _listableMaster = _typingRoguelikeProvider.TryGetFromId(bodyId).GetMaster();

            List<ITypingRoguelikeSingleSequenceMaster> _masterList = new List<ITypingRoguelikeSingleSequenceMaster>();
            foreach (var group in _listableMaster.GroupList)
            {
                List<ITypingMaster> typingMasterAvailableList = new List<ITypingMaster>();
                for (int i = 0; i < _typingProvider.Count; i++)
                {
                    if (_typingProvider.TryGetFromIndex(i).GetMaster().Group == group)
                    {
                        typingMasterAvailableList.Add(_typingProvider.TryGetFromIndex(i).GetMaster());
                    }

                }

                List<int> orderList;
                if (_listableMaster.SelectionMethod == TypingRoguelikeConst.SelectionMethod.Random)
                {

                    Const.RandomIndexList(out var randomizeList, typingMasterAvailableList.Count);
                    orderList = new List<int>();

                    for (int i = 0; i < _listableMaster.WaveCount; i++)
                    {
                        orderList.Add(randomizeList[i]);
                    }


                }
                else
                {
                    orderList = new List<int>();

                    for(int i = 0;  i< typingMasterAvailableList.Count;i++)
                    {
                        orderList.Add(i);
                    }
                }

                for (int i = 0; i < orderList.Count; i++)
                {
                    var typingMaster = typingMasterAvailableList[orderList[i]];
                    _masterList.Add(_factory.CreateSingleSequenceMaster(typingMaster, _listableMaster,
                       _restrictedCharDictionary.TryGetValue(bodyId, out List<char> list) ? list : new List<char>()));
                }
            }

            Log.Comment("TRlSingleSequenceMaster生成終了。長さ：" + _masterList.Count);
            return _masterList;
        }


        public void ResetRegistration()
        {
            _restrictedCharDictionary = new Dictionary<string, List<char>>();
        }
        public void RegisterStageMaster(string stageId, List<char> addedRestrictedCharList)
        {
            _restrictedCharDictionary.Add(stageId, addedRestrictedCharList);
        }
    }
}