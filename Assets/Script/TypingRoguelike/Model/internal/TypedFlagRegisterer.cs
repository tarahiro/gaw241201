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
        List<Pair>  _registeringDictionary = new List<Pair>();
        List<Pair> _fixedDictionary = new List<Pair>();

        public void StartRegister(string key)
        {
            _registeringDictionary.Add(new Pair(key,""));
        }

        public void EndRegister(string key)
        {
            var pair = _registeringDictionary.Find(x => x.key == key);
            pair.IsPreparedFix = true;
        }

        public void End()
        {
            //“o˜^
        }

        public void OnCharAdded(char c)
        {
            List<Pair> _removePairList = new List<Pair>();

            foreach (var pair in _registeringDictionary)
            {
                if (pair.IsCatchedDummyChar)
                {
                    pair.value = pair.value + c;
                    if (pair.IsPreparedFix)
                    {
                        _fixedDictionary.Add(pair);
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
                _registeringDictionary.Remove(pair);
            }
        }

        private class Pair
        {
            public string key;
            public string value;
            public bool IsCatchedDummyChar;
            public bool IsPreparedFix;

            public Pair(string key, string value)
            {
                this.key = key;
                this.value = value;
                IsCatchedDummyChar = false;
                IsPreparedFix = false;
            }
        }
    }
}