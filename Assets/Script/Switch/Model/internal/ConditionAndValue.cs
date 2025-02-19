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
    [System.Serializable]
    public class ConditionAndValue
    {
        [SerializeField] string _conditionId;
        [SerializeField] string _value;

        public string ConditionId => _conditionId;
        public string Value => _value;

        public ConditionAndValue(string conditionId, string value) { _conditionId= conditionId; _value= value; }
    }
}