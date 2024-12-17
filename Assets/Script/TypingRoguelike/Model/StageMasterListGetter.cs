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
    public class StageMasterListGetter : IGroupMasterGettable<IStageMaster>
    {
        [Inject] ITypingRoguelikeMasterDataProvider _typingRoguelikeMasterDataProvider;


        public List<IStageMaster> GetGroupMaster(string bodyId)
        {
            Log.Comment("StageMasterê∂ê¨äJén");

            var returnable = new List<IStageMaster>();

            var masterList = new List<ITypingRoguelikeMaster>();
            for(int i = 0; i < _typingRoguelikeMasterDataProvider.Count;i++)
            {
                var master = _typingRoguelikeMasterDataProvider.TryGetFromIndex(i).GetMaster();
                if (master.Group == bodyId)
                {
                    masterList.Add(master);
                }
            }

            for(int i = 0; i < masterList.Count; i++)
            {
                returnable.Add(new StageMaster(i, masterList[i].Id, bodyId, masterList[i].WaveCount, masterList[i].RestrictionIdList));
            }

            return returnable;

        }
    }
}