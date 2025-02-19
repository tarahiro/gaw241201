using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEditor.VersionControl;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class ConditionJudgerSimple : ISwitchConditionJudger
    {
        public bool IsMatch(string conditionValue, string targetValue)
        {
            return conditionValue.ToLower() == targetValue.ToLower();
        }
    }
}