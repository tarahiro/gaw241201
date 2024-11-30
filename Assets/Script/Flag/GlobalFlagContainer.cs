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
        Dictionary<FlagConst.Key ,string> _dictionary = new Dictionary<FlagConst.Key, string>();

        public void RegisterFlag(FlagConst.Key key, string value)
        {
            Log.Comment(key + "," + value +"‚ÌFlag‚ð“o˜^");
            _dictionary.Add(key, value);
        }

        public string GetFlag(FlagConst.Key key)
        {
            return _dictionary[key];
        }

    }
}