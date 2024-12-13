using Cysharp.Threading.Tasks;
using gaw241201.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.MasterData;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class AvaliableLeetMasterDataProvider
    {
        [Inject] ILeetMasterDataProvider _masterDataProvider;
        [Inject] IAchievableMasterFlagProvider<ILeetMaster> _masterFlagProvider;

        public List<ILeetMaster> GetAvailableLeetMasterDataList()
        {

            List<ILeetMaster> _returnableList = new List<ILeetMaster>();

            for (int i = 0; i < _masterDataProvider.Count; i++)
            {
                if (_masterFlagProvider.IsContainskey(_masterDataProvider.TryGetFromIndex(i).Id))
                {
                    _returnableList.Add(_masterDataProvider.TryGetFromIndex(i).GetMaster());
                }
            }

            return _returnableList;
        }
    }
}