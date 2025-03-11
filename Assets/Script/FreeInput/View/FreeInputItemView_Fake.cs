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
    public abstract class FreeInputItemView_Fake : MonoBehaviour
    {
        [SerializeField] protected List<InputCharacter> _inputCharacterList;
        [SerializeField] GameObject _enterKeyObject;

        protected int _index = 0;
        bool _isEndLoop = false;
        InputCharacter _currentItem => _inputCharacterList[_index];
        IFreeInputMessagePublisher _messagePublisher;

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

        private void Awake()
        {

        }

        private void Start()
        {
            _enterKeyObject.SetActive(false);
        }

        public void Construct(IFreeInputMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public async UniTask Enter(CancellationToken ct)
        {
            foreach (var item in _inputCharacterList)
            {
                item.Construct(_messagePublisher);
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
        }
    }
}