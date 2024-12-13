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
    public class LeetCharData
    {
        public char LeetedChar { get; set; }

        public List<string> ReplaceToStringList { get; set; }

        public LeetCharData(char leetedChar,List<string> replaceToString)
        {
            LeetedChar = leetedChar;
            ReplaceToStringList = replaceToString;
        }
    }
}