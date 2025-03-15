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
    public class CharJudgerTime : ICharJudger
    {
        //��U��24���Ԑ��̂�
        //12���Ԑ����Ȃ�AView���ς���K�v������Binput���̌���ύX�͍l���Ȃ��Ă������Ǝv��


        FreeInputIndexer _indexer;
        FreeInputUnfixedText _unfixedText;

        public CharJudgerTime(FreeInputIndexer indexer,FreeInputUnfixedText unfixedText)
        {
            _indexer = indexer;
            _unfixedText = unfixedText;
        }

        public bool IsCharAvailable(char c)
        {
            if (_indexer.IsFocusExist)
            {
                if(char.IsNumber(c))
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
                                char prevCharacter = _unfixedText.GetUnfixedText()[index - 1];
                                if (int.Parse(prevCharacter.ToString()) < 2)
                                {
                                    return true;
                                }
                                else
                                {
                                    return i <= 3;
                                }

                            case 2:
                                return i <= 5;

                            case 3:
                                return true;

                            case 4:
                                return i <= 5;

                            case 5:
                                return true;

                            default:
                                Log.DebugLog("�s���Ȓl�ł�");
                                return false;

                        }
                    }
                    else
                    {
                        Log.DebugLog("�s���Ȓl�ł�");
                        return false;
                    }
                }
                else
                {
                    Log.DebugLog("�s���Ȓl�ł�");
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