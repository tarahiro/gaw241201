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
    public class SimpleGroupMasterGetter<T> : IGroupMasterGettable<T> where T : IIdentifiable, IIndexable, IGroupable
    {
        [Inject] IMasterDataProvider<IMasterDataRecord<T>> _masterDataProvider;

        public List<T> GetGroupMaster(string bodyId)
        {
            List<T> _thisGroup = new List<T>();
            for (int i = 0; i < _masterDataProvider.Count; i++)
            {
                if (_masterDataProvider.TryGetFromIndex(i).GetMaster().Group == bodyId)
                {
                    _thisGroup.Add(_masterDataProvider.TryGetFromIndex(i).GetMaster());
                }
            }
            return _thisGroup;
        }

    }
}