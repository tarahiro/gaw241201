using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class FreeInputDayTimeView : FreeInputItemView
    {
        [SerializeField] List<InputCharacter> _inputCharacterList;
        [SerializeField] GameObject _enterKeyObject;

        int _index = 0;

        private void Start()
        {
            _enterKeyObject.SetActive(false);
        }

        public override async UniTask Enter(CancellationToken ct)
        {
            while (!IsCharInputFinish() && !ct.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                CheckInput();
            }

            _enterKeyObject.SetActive(true);
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Return),cancellationToken: ct);

            Exit();
        }

        bool IsCharInputFinish()
        {
            return _index == _inputCharacterList.Count;
        }

        void CheckInput()
        {
            foreach (var item in _keyCodeList)
            {
                if (Input.GetKeyDown(item))
                {
                    int num = GetNumberFromInput(item);

                    if (AcceptNumber(_index, num))
                    {
                        _inputCharacterList[_index].SetCharacter(num.ToString()[0]);
                        if (_index < _inputCharacterList.Count)
                        {
                            _index++;
                        }
                    }
                }
            }
        }

        private void Exit()
        {
            _enterKeyObject.SetActive(false);
            string s = "";
            foreach (var item in _inputCharacterList)
            {
                item.TryGetCharacter(out char c);
                s += c;
            };
            _exited.OnNext(s);
        }



        List<KeyCode> _keyCodeList = new List<KeyCode>()
        {
            KeyCode.Alpha0,
            KeyCode.Alpha1,
            KeyCode.Alpha2, KeyCode.Alpha3,
            KeyCode.Alpha4, KeyCode.Alpha5,
            KeyCode.Alpha6, KeyCode.Alpha7,
            KeyCode.Alpha8, KeyCode.Alpha9,
        };


        int GetNumberFromInput(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.Alpha0: return 0;
                case KeyCode.Alpha1: return 1;
                case KeyCode.Alpha2: return 2;
                case KeyCode.Alpha3: return 3;
                case KeyCode.Alpha4: return 4;
                case KeyCode.Alpha5: return 5;
                case KeyCode.Alpha6: return 6;
                case KeyCode.Alpha7: return 7;
                case KeyCode.Alpha8: return 8;
                case KeyCode.Alpha9: return 9;
                default: return -1;
            }
        }

        bool AcceptNumber(int index, int i)
        {
            switch (index)
            {
                case 0:
                    return i <= 2;

                case 1:
                    Log.DebugAssert(_inputCharacterList[index - 1].TryGetCharacter(out var c));
                    if (int.Parse(c.ToString()) < 2)
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
                    Log.DebugLog("•s³‚È’l‚Å‚·");
                    return false;

            }
        }
    }
}