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
    public class TypingRoguelikeConditionProvider
    {

        ITypingRoguelikeMaster _master;

        public bool IsEnableRestriction()
        {
            return _master.IsEnableRestriction;
        }

        public bool IsEnableTimeUp() { return _master.IsEnableTimeUp; }

        public bool IsEnableWave() { return _master.IsEnableWave; }

        public bool IsEnableScore() { return _master.IsEnableScore; }

        public void Initialize(ITypingRoguelikeMaster master)
        {
            _master = master;
        }
    }
}