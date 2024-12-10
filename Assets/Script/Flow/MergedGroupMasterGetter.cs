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
    public class MergedGroupMasterGetter<T,U> : IGroupMasterGettable<T> where T : IIdentifiable, IIndexable, IGroupable where U : IIdentifiable, IIndexable,IGroupListable
    {
        [Inject] IMasterDataProvider<IMasterDataRecord<T>> _groupableProvider;
        [Inject] IMasterDataProvider<IMasterDataRecord<U>> _groupListableProvider;


        public List<T> GetGroupMaster(string bodyId)
        {
            U _listableMaster = _groupListableProvider.TryGetFromId(bodyId).GetMaster();

            List<T> _masterList = new List<T>();
            foreach(var group in _listableMaster.GroupList)
            {
                for(int i = 0; i < _groupableProvider.Count; i++)
                {
                    if(_groupableProvider.TryGetFromIndex(i).GetMaster().Group == group)
                    {
                        _masterList.Add(_groupableProvider.TryGetFromIndex(i).GetMaster());
                    }
                }
            }

            return _masterList;
        }
    }
}