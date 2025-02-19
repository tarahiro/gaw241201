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
    public class ConditionJudgerFactory : IConditionJudgerFactory
    {
        const char c_spritter = ':';

        [Inject] IGlobalFlagProvider _provider;

        public ISwitchConditionJudger Create(string ConditionKey)
        {
            string[] spritted = ConditionKey.Split(c_spritter);
            Log.DebugLog(spritted.Length);
            if (spritted.Length > 1)
            {
                Log.DebugLog(spritted[0]);
                switch (spritted[0])
                {
                    case "Key":
                        return new ConditionJudgerFlag(EnumUtil.KeyToType<FlagConst.Key>(spritted[1]), _provider);

                    case "Else":
                        return new ConditionJudgerElse();

                    default:
                         Log.DebugAssert(spritted[0] + "ÇÕïsê≥Ç»ílÇ≈Ç∑");
                        return null;

                }
            }
            else
            {
                return new ConditionJudgerSimple();
            }
        }
    }
}