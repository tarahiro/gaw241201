using Cysharp.Threading.Tasks;
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
    public class AvaliableMasterDataProvider<T> : IAvailableMasterDataProvider<T> where T : IIdentifiable,IIndexable
    {
        [Inject] IMasterDataProvider<T> _masterDataProvider;
        [Inject] IAchievableMasterFlagProvider _masterFlagProvider;

        protected virtual FlagConst.ContainableMasterKey _containableMasterKey { get; }

        public List<T> GetAvailableMasterDataList()
        {

            List<T> _returnableList = new List<T>();

            for (int i = 0; i < _masterDataProvider.Count; i++)
            {
                if (_masterFlagProvider.IsContainskey(_containableMasterKey, _masterDataProvider.TryGetFromIndex(i).Id))
                {
                    _returnableList.Add(_masterDataProvider.TryGetFromIndex(i));
                }
            }

            return _returnableList;
        }
    }
}