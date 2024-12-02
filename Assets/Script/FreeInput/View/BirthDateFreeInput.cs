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
    public class BirthDateFreeInput : FreeInputItemView
    {
        protected override string defaultValue { get; set; } = "190000";
        protected override bool IsMainKeyListContains(int index, KeyCode key)
        {
            if (TryGetStringFromInput(key, out var s))
            {
                if (int.TryParse(s, out var i))
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
                            Log.DebugAssert(_inputCharacterList[index - 1].TryGetCharacter(out var c));
                            if (int.Parse(c.ToString()) < 1)
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
                            Log.DebugLog("�s���Ȓl�ł�");
                            return false;
                    }

                }

            }
            return false;
        }

        protected override bool IsAcceptEnter()
        {
            return false;
        }

        protected override bool IsForceEnd()
        {
            return _index >= 7;
        }
    }
}