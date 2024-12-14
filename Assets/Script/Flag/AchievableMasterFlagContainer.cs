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
    public class AchievableMasterFlagContainer : IAchievableMasterFlagProvider
    {
        Dictionary<FlagConst.ContainableMasterKey, List<string>> _dictionary = new Dictionary<FlagConst.ContainableMasterKey, List<string>>() {
            { FlagConst.ContainableMasterKey.Leet,new List<string>()
        {
            "a","i"
        } }, { FlagConst.ContainableMasterKey.Word,new List<string>()
        {
            "cat","alive"
        } },
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

    }
}