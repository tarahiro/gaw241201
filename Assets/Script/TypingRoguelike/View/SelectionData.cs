using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SelectionData
    {
        public string ReplacedString;
        public string StringReplaceTo;

        public SelectionData(string ReplacedString, string StringReplaceTo)
        {
            this.ReplacedString = ReplacedString;
            this.StringReplaceTo = StringReplaceTo;
        }
    }
}