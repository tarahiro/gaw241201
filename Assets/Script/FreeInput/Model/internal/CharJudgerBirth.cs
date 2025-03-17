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
    public class CharJudgerBirth : ICharJudger
    {
        FreeInputIndexer _indexer;
        FreeInputUnfixedText _unfixedText;

        public CharJudgerBirth(FreeInputIndexer indexer, FreeInputUnfixedText unfixedText)
        {
            _indexer = indexer;
            _unfixedText = unfixedText;
        }

        public bool IsCharAvailable(char c)
        {
            if (_indexer.IsFocusExist)
            {
                if (char.IsNumber(c))
                {
                    var i = char.GetNumericValue(c);
                    int index = _indexer.Index;
                    if (i >= 0)
                    {
                        switch (index)
                        {
                            case 0:
                                return i <= 2;

                            case 1:
                                return true;

                            case 2:
                                return true;

                            case 3:
                                return true;

                            case 4:
                                return i <= 1;

                            case 5:
                                char prevCharacter = _unfixedText.GetUnfixedText()[index - 1];
                                if (int.Parse(prevCharacter.ToString()) < 1)
                                {
                                    return true;
                                }
                                else
                                {
                                    return i <= 2;
                                }

                            case 6:
                                return i <= 3;

                            default:
                                Log.DebugLog("不正な値です");
                                return false;
                        }

                    }
                    else
                    {
                        Log.DebugLog("不正な値です");
                        return false;
                    }
                }
                else
                {
                    Log.DebugLog("不正な値です");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}