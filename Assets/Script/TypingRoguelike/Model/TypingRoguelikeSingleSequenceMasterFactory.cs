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

        [Inject] TypingRoguelikeConditionProvider _conditionProvider;

        public ITypingRoguelikeSingleSequenceMaster CreateSingleSequenceMaster(ITypingMaster typingMaster, ITypingRoguelikeMaster typingRoguelikeMaster, List<char> restrictionList)
        {
            _conditionProvider.Initialize(typingRoguelikeMaster);
            return new TypingRoguelikeSingleSequenceMaster(typingMaster, _conditionProvider, restrictionList, typingRoguelikeMaster.TimePerChar);
        }
    }
}