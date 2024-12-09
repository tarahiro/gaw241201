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
    public abstract class FreeInputItemView : MonoBehaviour
    {
        [SerializeField] protected List<InputCharacter> _inputCharacterList;
        [SerializeField] GameObject _enterKeyObject;

        protected int _index = 0;
        bool _isEndLoop = false;
        InputCharacter _currentItem => _inputCharacterList[_index];
        IGazable _gazable;

        protected Subject<string> _exited = new Subject<string>();
        public IObservable<string> Exited => _exited;

        protected virtual string defaultValue { get; set; }

        protected virtual void Initialize()
        {

        }

        protected virtual bool IsAcceptEnter()
        {
            return true;
        }

        protected virtual bool IsForceEnd()
        {
            return false;
        }
        protected virtual bool IsInputCharValid(int index, char c)
        {
            return true;
        }

        private void Start()
        {
            _enterKeyObject.SetActive(false);
        }

        public void Construct(IGazable gazable)
        {
            _gazable = gazable;
        }

        public async UniTask Enter(CancellationToken ct)
        {
            foreach (var item in _inputCharacterList)
            {
                item.Enter(_gazable);
            }
            _currentItem.Focus();
            ct.Register(() => Exit(ct));

            while (!_isEndLoop)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                CheckInput();
            }

            Exit(ct);
        }

        void AcceptEnter()
        {
            _enterKeyObject.SetActive(true);
        }
        void NotAcceptEnter()
        {
            _enterKeyObject.SetActive(false);
        }

        bool IsMainInputAccept()
        {
            return _index < _inputCharacterList.Count;
        }


        void CheckInput()
        {
            if (IsMainInputAccept())
            {
                for (int i = 0; i < Input.inputString.Length; i++)
                {
                    char c = Input.inputString[i];

                    Log.Comment("このクラスのキーリストに入ってるか確認");
                    if (IsInputCharValid(_index, c))
                    {
                        _currentItem.SetCharacter(ConvertChar(c));
                        if (_index < _inputCharacterList.Count)
                        {
                            TryGoNextCharacter();
                        }
                    }
                }
            }
            if (_index > 0)
            {
                if (KeyInputUtil.IsKeyDown(KeyCode.Backspace))
                {
                    GoBackCharacter();
                }
            }

            if (IsAcceptEnter())
            {
                if (KeyInputUtil.IsKeyDown(KeyCode.Return))
                {
                    EndLoop();
                }
            }
        }

        void EndLoop()
        {
            _isEndLoop = true;
        }

        void TryGoNextCharacter()
        {
            _currentItem.UnFocus();
            _index++;
            if (_index < _inputCharacterList.Count)
            {
                _currentItem.Focus();
            }

            if (IsForceEnd())
            {
                EndLoop();
            }

            if (IsAcceptEnter())
            {
                AcceptEnter();
            }
        }

        void GoBackCharacter()
        {
            if (_index < _inputCharacterList.Count)
            {
                _currentItem.UnFocus();
            }
            _index--;
            _currentItem.ClearCharacter();
            _currentItem.Focus();
            if (!IsAcceptEnter())
            {
                NotAcceptEnter();
            }
        }

        private void Exit(CancellationToken ct)
        {
            NotAcceptEnter();
            if(_index < _inputCharacterList.Count)
            {
                _currentItem.UnFocus();
            }

            string s = "";
            if (ct.IsCancellationRequested)
            {
                s = defaultValue;
            }
            else
            {

                foreach (var item in _inputCharacterList)
                {
                    if (item.TryGetCharacter(out char c))
                    {
                        s += c;
                    }
                };
            }

            _exited.OnNext(s);

        }

     




        protected char ConvertChar(char c)
        {
            return char.ToUpper(c);

            /*
            switch (keyCode)
            {
                case KeyCode.Alpha0: s = "0"; return true;
                case KeyCode.Alpha1: s = "1"; return true;
                case KeyCode.Alpha2: s = "2"; return true;
                case KeyCode.Alpha3: s = "3"; return true;
                case KeyCode.Alpha4: s =  "4"; return true;
                case KeyCode.Alpha5: s =  "5"; return true;
                case KeyCode.Alpha6: s =  "6"; return true;
                case KeyCode.Alpha7: s =  "7"; return true;
                case KeyCode.Alpha8: s =  "8"; return true;
                case KeyCode.Alpha9: s =  "9"; return true;

                case KeyCode.A: s =  "A"; return true;
                case KeyCode.B: s =  "B"; return true;
                case KeyCode.C: s =  "C"; return true;
                case KeyCode.D: s =  "D"; return true;
                case KeyCode.E: s =  "E"; return true;
                case KeyCode.F: s =  "F"; return true;
                case KeyCode.G: s =  "G"; return true;
                case KeyCode.H: s =  "H"; return true;
                case KeyCode.I: s =  "I"; return true;
                case KeyCode.J: s =  "J"; return true;
                case KeyCode.K: s =  "K"; return true;
                case KeyCode.L: s =  "L"; return true;
                case KeyCode.M: s =  "M"; return true;
                case KeyCode.N: s =  "N"; return true;
                case KeyCode.O: s =  "O"; return true;
                case KeyCode.P: s =  "P"; return true;
                case KeyCode.Q: s =  "Q"; return true;
                case KeyCode.R: s =  "R"; return true;
                case KeyCode.S: s =  "S"; return true;
                case KeyCode.T: s =  "T"; return true;
                case KeyCode.U: s =  "U"; return true;
                case KeyCode.V: s =  "V"; return true;
                case KeyCode.W: s =  "W"; return true;
                case KeyCode.X: s =  "X"; return true;
                case KeyCode.Y: s =  "Y"; return true;
                case KeyCode.Z: s =  "Z"; return true;

                default: 
                    s = "error";
                    return false;
            }
            */
        }
    }
}