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

        public bool IsEnableRestriction(ITypingRoguelikeMaster master)
        {
            return master.IsEnableRestriction;
        }

        public bool IsEnableTimeUp(ITypingRoguelikeMaster master) { return master.IsEnableTimeUp; }

        public bool IsEnableWave(ITypingRoguelikeMaster master) { return master.IsEnableWave; }

        public bool IsEnableScore(ITypingRoguelikeMaster master) { return master.IsEnableScore; }
    }
}