using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using gaw241201.Model;

namespace gaw241201
{
    public class RestrictionGenerator : IRestrictionGenerator
    {
        [Inject] IRestrictionMasterDataProvider _masterDataProvider;

        public List<char> GenerateRestriction(List<char> presentRestrictionList, List<string> addedRestrictionIdList)
        {
            List<char> returnableList = new List<char>();
            foreach(var c in presentRestrictionList)
            {
                returnableList.Add(c);
            }

            for(int i = 0; i < addedRestrictionIdList.Count; i++)
            {
                var master = _masterDataProvider.TryGetFromId(addedRestrictionIdList[i]).GetMaster();
                List<char> availableList = new List<char>();

                foreach(var c in master.RestrictedCharList)
                {
                    if (!availableList.Contains(c)) availableList.Add(c);
                }

                if(availableList.Count > 0)
                {
                    returnableList.Add(availableList[UnityEngine.Random.Range(0, availableList.Count)]);
                }
            }

            return returnableList;
        }
    }
}