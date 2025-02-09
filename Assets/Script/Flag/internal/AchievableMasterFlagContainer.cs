using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
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
    public class AchievableMasterFlagContainer : IAchievableMasterFlagProvider, IAchievableMasterFlagRegisterer
    {
        Dictionary<FlagConst.ContainableMasterKey, List<string>> _dictionary = new Dictionary<FlagConst.ContainableMasterKey, List<string>>() {
            { FlagConst.ContainableMasterKey.Leet,new List<string>() }, { FlagConst.ContainableMasterKey.Word,new List<string>() },
        };

        public List<string> RegisteredId(FlagConst.ContainableMasterKey key)
        {
            return _dictionary[key];
        }

        public bool IsContainskey(FlagConst.ContainableMasterKey key, string Id)
        {
            if (_dictionary.ContainsKey(key))
            {
                return _dictionary[key].Contains(Id);
            }

            return false;
        }

        public void RegisterFlag(FlagConst.ContainableMasterKey key, string id)
        {
            if (IsContainskey(key, id))
            {
                Debug.LogWarning(key + id + "は登録済みです");
            }
            else
            {
                Log.Comment("獲得マスター更新 ; " + key + id);
                _dictionary[key].Add(id);
            }
        }

    }
}