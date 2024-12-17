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
using System.Linq;

namespace gaw241201
{
    public class TypingRoguelikeSingleSequenceMasterFactory : ITypingRoguelikeSingleSequenceMasterFactory
    {
        [Inject] IRestrictionMasterDataProvider _restrictionMasterDataProvider;

        public ITypingRoguelikeSingleSequenceMaster CreateSingleSequenceMaster(ITypingMaster typingMaster, ITypingRoguelikeMaster typingRoguelikeMaster, List<char> restrictionList)
        {
            /*
            for (int i = 0; i < 3; i++)
            {
                int num = UnityEngine.Random.Range(0, 26);
                char letter = (char)('a' + num - 1); // 'a'のASCIIコードから計算
                chars.Add(letter);
            }
            */
            return new TypingRoguelikeSingleSequenceMaster(typingMaster, restrictionList, typingRoguelikeMaster.TimePerChar);

        }
    }
}