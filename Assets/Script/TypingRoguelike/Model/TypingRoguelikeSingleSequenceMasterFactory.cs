using Cysharp.Threading.Tasks;
using gaw241201.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using Unity.Plastic.Antlr3.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class TypingRoguelikeSingleSequenceMasterFactory : ITypingRoguelikeSingleSequenceMasterFactory
    {
        public ITypingRoguelikeSingleSequenceMaster CreateSingleSequenceMaster(ITypingMaster typingMaster, ITypingRoguelikeMaster typingRoguelikeMaster, IRestrictionMaster restrictionMaster)
        {

            //Fake アルファベットの中からランダムで出す
            List<char> chars = new List<char>();


            for (int i = 0; i < 3; i++)
            {
                int num = UnityEngine.Random.Range(0, 26);
                char letter = (char)('a' + num - 1); // 'a'のASCIIコードから計算
                chars.Add(letter);
            }
            return new TypingRoguelikeSingleSequenceMaster(typingMaster, chars, typingRoguelikeMaster.TimePerChar);

        }
    }
}