using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class TypedFlagRegisterer
    {
        [Inject] TypedFlagContainer _typedFlagContainer;

        List<Pair>  _registeringPair = new List<Pair>();
        List<Pair> _fixedPair = new List<Pair>();

        public void StartRegister(string key)
        {
            _registeringPair.Add(new Pair(EnumUtil.KeyToType<TypedFlagContainer.TypedKey>(key) ,""));
        }

        public void EndRegister(string key)
        {
            var Key = EnumUtil.KeyToType<TypedFlagContainer.TypedKey>(key);
            var pair = _registeringPair.Find(x => x.key == Key);
            pair.IsPreparedFix = true;
        }

        public void OnCharAdded(char c)
        {
            List<Pair> _removePairList = new List<Pair>();

            foreach (var pair in _registeringPair)
            {
                if (pair.IsCatchedDummyChar)
                {
                    pair.value = pair.value + c;
                    if (pair.IsPreparedFix)
                    {
                        _fixedPair.Add(pair);
                        Log.Comment("Fix:" + pair.key + pair.value);

                        _removePairList.Add(pair);
                    }
                }
                else
                {
                    pair.IsCatchedDummyChar = true;
                }
            }

            foreach (var pair in _removePairList)
            {
                _registeringPair.Remove(pair);
            }
        }

        public void End()
        {
            //“o˜^
            for(int i = 0; i < _fixedPair.Count; i++)
            {
                switch (_fixedPair[i].key)
                {
                    case TypedFlagContainer.TypedKey.TypedName:
                        _typedFlagContainer.RegisterTypedName(_fixedPair[i].value); 
                        break;

                    case TypedFlagContainer.TypedKey.Holder:
                        _typedFlagContainer.RegisterHolder(_fixedPair[i].value);
                        break;
                    case TypedFlagContainer.TypedKey.Animal:
                        _typedFlagContainer.RegisterAnimal(_fixedPair[i].value);
                        break;

                    case TypedFlagContainer.TypedKey.Right:
                        _typedFlagContainer.RegisterRight(_fixedPair[i].value);
                        break;
                    default:
                        Log.DebugWarning("—LŒø‚ÈƒL[‚ª‚ ‚è‚Ü‚¹‚ñ");
                        break;
                }
            }

            Log.DebugAssert(_registeringPair.Count == 0);
            _registeringPair = new List<Pair>();
            _fixedPair = new List<Pair>();
        }

        private class Pair
        {
            public TypedFlagContainer.TypedKey key;
            public string value;
            public bool IsCatchedDummyChar;
            public bool IsPreparedFix;

            public Pair(TypedFlagContainer.TypedKey key, string value)
            {
                this.key = key;
                this.value = value;
                IsCatchedDummyChar = false;
                IsPreparedFix = false;
            }
        }
    }
}