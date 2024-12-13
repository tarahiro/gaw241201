using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class LeetDataListFactory
    {
        public List<LeetCharData> Create(List<ILeetMaster> leetMasterList)
        {
            List<LeetCharData> _returnableList = new List<LeetCharData>();
            foreach(var leetMaster in leetMasterList)
            {
                _returnableList.Add(new LeetCharData(leetMaster.LeetedChar, leetMaster.ReplaceToStringList.ToList()));
            }

            return _returnableList;
        }
    }
}