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
    public class StageMasterRegisteredRestrictedCharList : StageMaster, IStageMasterRegisteredRestrictedCharList
    {

        public List<char> RestrictedCharList { get; set; }

        public StageMasterRegisteredRestrictedCharList(int index, string id, string group, int waveCount, string[] addedRestrictedCharList, List<char> charList) : base (index,  id,  group,  waveCount, addedRestrictedCharList)
        {
            RestrictedCharList = charList;
        }
    }
}