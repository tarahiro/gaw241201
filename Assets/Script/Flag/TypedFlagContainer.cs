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
    public class TypedFlagContainer
    {

        Dictionary<TypedKey,string> _typedFlagDictionary = new Dictionary<TypedKey,string>();

        public void Register(TypedKey key, string value)
        {
            if (_typedFlagDictionary.ContainsKey(key))
            {
                _typedFlagDictionary[key] = value;
            }
            else
            {
                _typedFlagDictionary.Add(key, value);
            }
        }

        public bool TryGetValue(TypedKey type, out string value)
        {
            return _typedFlagDictionary.TryGetValue(type, out value);
        }


        public enum TypedKey
        {
            Name,
            Holder,
            Animal,
            Direction,
        }
    }
}