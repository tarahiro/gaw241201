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
    public class WordData
    {
        public string StringReplaceTo { get; set; }
        public string Tag { get; set; }

        public WordData(string stringReplaceTo, string tag)
        {
            StringReplaceTo = stringReplaceTo;
            Tag = tag;
        }
    }
}