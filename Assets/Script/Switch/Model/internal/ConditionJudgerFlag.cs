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
    public class ConditionJudgerFlag : ISwitchConditionJudger
    {
        FlagConst.Key _key;
        IGlobalFlagProvider _provider;

       public ConditionJudgerFlag(FlagConst.Key key, IGlobalFlagProvider provider)
        {
            _key = key;
            _provider = provider;
        }

        public bool IsMatch(string conditionValue,string targetValue)
        {
            return _provider.GetFlag(_key).ToLower() == targetValue.ToLower();
        }
    }
}