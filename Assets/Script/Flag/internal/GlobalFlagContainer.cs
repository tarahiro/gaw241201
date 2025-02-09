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
        [Inject] FlagPublisher _flagPublisher;

        Dictionary<FlagConst.Key ,string> _dictionary = new Dictionary<FlagConst.Key, string>();

        public void RegisterFlag(FlagConst.Key key, string value)
        {
            Log.Comment(key + "," + value +"‚ÌFlag‚ğ“o˜^");
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, value);
            }
            else
            {
                _dictionary[key] = value;
            }

            _flagPublisher.PublishEvent(key, value);
        }

        public string GetFlag(FlagConst.Key key)
        {
            if (_dictionary.ContainsKey(key))
            {
                return _dictionary[key];
            }
            else
            {
                Log.DebugWarning(key + "‚ª“o˜^‚³‚ê‚Ä‚¢‚Ü‚¹‚ñB‰Šú’l‚ğ•Ô‚µ‚Ü‚·");
                return FlagConst.InitialValue(key);
            }
        }

        public bool IsContainskey(FlagConst.Key key)
        {
            return _dictionary.ContainsKey(key);
        }


    }
}