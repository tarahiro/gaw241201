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
    public class SelectionDataWithIndex
    {
        public ReplaceData ReplaceData { get; private set; }
        public int Index { get; private set; }

        public SelectionDataWithIndex(ReplaceData replaceData, int index)
        {
            ReplaceData = replaceData;
            Index = index;
        }
    }
}