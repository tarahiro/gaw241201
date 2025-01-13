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

        public string JpText { get; private set; }

        public string RomanText { get; private set; }

        public List<char> RestrictedCharList { get; private set; }

        public float Time { get; private set; }
        public TypingRoguelikeSingleSequenceMaster(ITypingMaster typingMaster, List<char> restrictedCharList, float time)
        {
            Index = typingMaster.Index;
            Id = typingMaster.Id;
            Group = typingMaster.Group;
            JpText = typingMaster.JpText;
            RomanText = typingMaster.RomanText;
            RestrictedCharList = restrictedCharList;
            Time = time;
        }
    }
}