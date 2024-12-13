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
    public class LeetInputJudger : IKeyInputJudger, ILeetDataUserView
    {
        //本来はViewが持つべきではないが、ここでデータを持つことにする
        //Romanのリファクタがしんどいため
        List<LeetCharData> _charDataList;

        public void Initialize(List<LeetCharData> charDataList)
        {
            _charDataList = charDataList;
        }


        public bool IsKeyInputCorrect(char inputChar, int _charIndex, List<char> _questionCharList)
        {
            char currentChar = _questionCharList[_charIndex];

            if (inputChar == '\0')
            {
                return false;
            }

            if (inputChar == currentChar)
            {
                return true;
            }


            foreach (var charData in _charDataList)
            {
                if(currentChar == charData.LeetedChar)
                {
                    foreach(var replaceToList in charData.ReplaceToStringList)
                    {
                        if(inputChar == replaceToList[0])
                        {
                            _questionCharList[_charIndex] = replaceToList[0];
                            for(int i = replaceToList.Length - 1; i >= 1; i--)
                            {
                                _questionCharList.Insert(_charIndex + 1, replaceToList[i]);
                            }
                            return true;
                        }
                    }
                }
            }



            return false;
        }
    }

}