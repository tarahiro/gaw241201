using Cysharp.Threading.Tasks;
using gaw241201.Model;
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
    public class TypingRoguelikeSingleSequenceMaster : ITypingRoguelikeSingleSequenceMaster
    {
        public int Index { get; private set; }

        public string Id { get; private set; }

        public string Group { get; private set; }

        public string DisplayText { get; private set; }

        public string QuestionText { get; private set; }

        public List<char> RestrictedCharList { get; private set; }

        public float Time { get; private set; }

        public TypingRoguelikeConditionProvider ConditionProvider { get; private set; }
        public TypingRoguelikeSingleSequenceMaster(ITypingMaster typingMaster, TypingRoguelikeConditionProvider conditionProvider, List<char> restrictedCharList, float time)
        {
            Index = typingMaster.Index;
            Id = typingMaster.Id;
            Group = typingMaster.Group;
            DisplayText = typingMaster.DisplayText;
            QuestionText = typingMaster.QuestionText;
            RestrictedCharList = restrictedCharList;
            Time = time;

            ConditionProvider = conditionProvider;
        }
    }
}