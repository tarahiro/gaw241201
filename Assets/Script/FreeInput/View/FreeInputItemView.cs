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

            while (!_isEndLoop && !ct.IsCancellationRequested)
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


        void CheckInput()
        {
            if (_index < _inputCharacterList.Count)
            {
                foreach (var item in _inputtableKeyList)
                {
                    if (Input.GetKeyDown(item))
                    {
                        string key = GetStringFromInput(item);

                        if (isAcceptKey(_index, key))
                        {
                            _currentItem.SetCharacter(key);
                            if (_index < _inputCharacterList.Count)
                            {
                                TryGoNextCharacter();
                            }
                        }
                    }
                }
            }

            if (_index > 0)
            {
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    GoBackCharacter();
                }
            }

            if (IsAcceptEnter())
            {
                if (Input.GetKey(KeyCode.Return))
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

     



        List<KeyCode> _inputtableKeyList = new List<KeyCode>()
        {
            KeyCode.Alpha0,
            KeyCode.Alpha1,
            KeyCode.Alpha2, 
            KeyCode.Alpha3,
            KeyCode.Alpha4, 
            KeyCode.Alpha5,
            KeyCode.Alpha6, 
            KeyCode.Alpha7,
            KeyCode.Alpha8, 
            KeyCode.Alpha9,

            KeyCode.A,
            KeyCode.B,
            KeyCode.C,
            KeyCode.D,
            KeyCode.E,
            KeyCode.F,
            KeyCode.G,
            KeyCode.H,
            KeyCode.I,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L,
            KeyCode.M,
            KeyCode.N,
            KeyCode.O,
            KeyCode.P,
            KeyCode.Q,
            KeyCode.R,
            KeyCode.S,
            KeyCode.T,
            KeyCode.U,
            KeyCode.V,
            KeyCode.W,
            KeyCode.X,
            KeyCode.Y,
            KeyCode.Z,
        };


        string GetStringFromInput(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.Alpha0: return "0";
                case KeyCode.Alpha1: return "1";
                case KeyCode.Alpha2: return "2";
                case KeyCode.Alpha3: return "3";
                case KeyCode.Alpha4: return "4";
                case KeyCode.Alpha5: return "5";
                case KeyCode.Alpha6: return "6";
                case KeyCode.Alpha7: return "7";
                case KeyCode.Alpha8: return "8";
                case KeyCode.Alpha9: return "9";

                case KeyCode.A: return "A";
                case KeyCode.B: return "B";
                case KeyCode.C: return "C";
                case KeyCode.D: return "D";
                case KeyCode.E: return "E";
                case KeyCode.F: return "F";
                case KeyCode.G: return "G";
                case KeyCode.H: return "H";
                case KeyCode.I: return "I";
                case KeyCode.J: return "J";
                case KeyCode.K: return "K";
                case KeyCode.L: return "L";
                case KeyCode.M: return "M";
                case KeyCode.N: return "N";
                case KeyCode.O: return "O";
                case KeyCode.P: return "P";
                case KeyCode.Q: return "Q";
                case KeyCode.R: return "R";
                case KeyCode.S: return "S";
                case KeyCode.T: return "T";
                case KeyCode.U: return "U";
                case KeyCode.V: return "V";
                case KeyCode.W: return "W";
                case KeyCode.X: return "X";
                case KeyCode.Y: return "Y";
                case KeyCode.Z: return "Z";

                default: return "error";
            }
        }
        protected virtual bool isAcceptKey(int index, string key)
        {
            return true;
        }
    }
}