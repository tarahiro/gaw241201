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
        [Inject] RoguelikeEnableFlagPublisher _roguelikeEnableFlagPublisher;

        Dictionary<FlagConst.Key ,string> _dictionary = new Dictionary<FlagConst.Key, string>();

        public void RegisterFlag(FlagConst.Key key, string value)
        {
            Log.Comment(key + "," + value +"のFlagを登録");
            if (!_dictionary.ContainsKey(key))
            {
                _dictionary.Add(key, value);
            }
            else
            {
                _dictionary[key] = value;
            }

            //別クラスに分けるかも。フラグの読み方をContainerが知るべきではないため
            if(key == FlagConst.Key.IsRoguelikeEnabled)
            {
                _roguelikeEnableFlagPublisher.PublishEvent(value == Tarahiro.Const.c_true);
            }
        }

        public string GetFlag(FlagConst.Key key)
        {
            if (_dictionary.ContainsKey(key))
            {
                return _dictionary[key];
            }
            else
            {
                Log.DebugWarning(key + "が登録されていません。初期値を返します");
                return FlagConst.InitialValue(key);
            }
        }

        public bool IsContainskey(FlagConst.Key key)
        {
            return _dictionary.ContainsKey(key);
        }


    }
}