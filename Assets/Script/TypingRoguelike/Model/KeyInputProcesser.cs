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
    public class KeyInputProcesser
    {
        public bool TryKeyProcess(char inputChar, int _charIndex, string _questionString, List<SelectionData> _selectionData, out List<SelectionData> selected)
        {
            selected = new List<SelectionData>();
            char currentChar = _questionString[_charIndex];

            if (inputChar == '\0')
            {
                return false;
            }

            if (inputChar == currentChar)
            {
                return true;
            }


            foreach (var charData in _selectionData)
            {
                if (inputChar == charData.StringReplaceTo[0])
                {
                    Log.Comment("SelectedDataŒŸo");
                    selected.Add(charData);
                }
            }

            return selected.Count > 0;

        }
    }
}