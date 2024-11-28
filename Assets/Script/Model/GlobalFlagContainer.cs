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
    public class GlobalFlagContainer: IGlobalFlagProvider,IGlobalFlagRegisterer
    {
        Dictionary<string ,string> _dictionary = new Dictionary<string ,string>();

        public void RegisterFlag(string key, string value)
        {
            Log.Comment(key + "," + value +"‚ÌFlag‚ð“o˜^");
            _dictionary.Add(key, value);
        }

        public string GetFlag(string key)
        {
            return _dictionary[key];
        }

    }
}