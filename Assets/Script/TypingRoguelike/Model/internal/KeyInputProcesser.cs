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
        [Inject] RomanInputProcesser _romanInputProcesser;


        public bool TryKeyProcess(char inputChar, int _charIndex, string _questionString, List<ReplaceData> _replaceDataList, out List<ReplaceData> replacedList)
        {
            if (_charIndex < _questionString.Length)
            {

                Log.Comment("判定開始:" + inputChar + " " + _questionString[_charIndex]);

                replacedList = new List<ReplaceData>();
                char currentChar = _questionString[_charIndex];

                if (inputChar == '\0')
                {
                    return false;
                }

                if (inputChar == currentChar)
                {
                    return true;
                }



                //ローマ字入力
                ReplaceData romanReplaceData = _romanInputProcesser.IsKeyInputCorrect(inputChar, _charIndex, _questionString);
                if (romanReplaceData != null)
                {
                    replacedList.Add(romanReplaceData);
                }


                foreach (var replaceData in _replaceDataList)
                {

                    if (inputChar == replaceData.StringReplaceTo[0])
                    {
                        Log.Comment("SelectedData検出");
                        replacedList.Add(replaceData);
                        continue;
                    }

                    //大文字・小文字処理
                    if (char.IsUpper(replaceData.StringReplaceTo[0]) && inputChar == char.ToLower(replaceData.StringReplaceTo[0]))
                    {
                        Log.Comment("SelectedData検出");
                        replacedList.Add(replaceData);
                        continue;
                    }

                }

                return replacedList.Count > 0;
            }
            else
            {
                replacedList = new List<ReplaceData>();
                return false;
            }

        }
    }
}