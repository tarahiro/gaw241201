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
    public class TypingRoguelikeSingleSequenceMasterGetter : IGroupMasterGettable<ITypingRoguelikeSingleSequenceMaster>
    {
        [Inject] IMasterDataProvider<IMasterDataRecord<ITypingMaster>> _typingProvider;
        [Inject] IMasterDataProvider<IMasterDataRecord<ITypingRoguelikeMaster>> _typingRoguelikeProvider;
        [Inject] IMasterDataProvider<IMasterDataRecord<IRestrictionMaster>> _restrictionProvider;

        public List<ITypingRoguelikeSingleSequenceMaster> GetGroupMaster(string bodyId)
        {
            Log.Comment("TRlSingleSequenceMaster生成開始");
            ITypingRoguelikeMaster _listableMaster = _typingRoguelikeProvider.TryGetFromId(bodyId).GetMaster();

            List<ITypingRoguelikeSingleSequenceMaster> _masterList = new List<ITypingRoguelikeSingleSequenceMaster>();
            foreach (var group in _listableMaster.GroupList)
            {
                for (int i = 0; i < _typingProvider.Count; i++)
                {
                    if (_typingProvider.TryGetFromIndex(i).GetMaster().Group == group)
                    {
                        Log.Comment(_typingProvider.TryGetFromIndex(i).GetMaster().JpText);
                        _masterList.Add(new TypingRoguelikeSingleSequenceMaster(
                            _typingProvider.TryGetFromIndex(i).GetMaster(),
                            _restrictionProvider.TryGetFromId(_listableMaster.RestrictionId).GetMaster().RestrictedCharList.ToList(),
                            _listableMaster.TimePerChar)
                            );
                    }
                }
            }

            Log.Comment("TRlSingleSequenceMaster生成終了。長さ：" + _masterList.Count);
            return _masterList;
        }
    }
}