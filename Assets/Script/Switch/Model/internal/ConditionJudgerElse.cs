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
    public class ConditionJudgerElse : ISwitchConditionJudger
    {
        public bool IsMatch(string s, string t)
        {
            return true;
        }
    }
}