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
    public class ReplaceData
    {
        public string ReplacedString;
        public string StringReplaceTo;

        public ReplaceData(string ReplacedString, string StringReplaceTo)
        {
            this.ReplacedString = ReplacedString;
            this.StringReplaceTo = StringReplaceTo;
        }
    }
}