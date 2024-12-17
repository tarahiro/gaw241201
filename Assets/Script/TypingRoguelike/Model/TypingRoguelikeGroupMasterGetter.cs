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

                Const.RandomIndexList(out var randomizeList, typingMasterAvailableList.Count);

                for(int i = 0;i < _listableMaster.WaveCount; i++)
                {
                    var typingMaster = typingMasterAvailableList[randomizeList[i]];
                    _masterList.Add(_factory.CreateSingleSequenceMaster(typingMaster, _listableMaster, 
                       _restrictedCharDictionary[bodyId]));
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