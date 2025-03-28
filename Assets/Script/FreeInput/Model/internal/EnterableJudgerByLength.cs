using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
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
    public class EnterableJudgerByLength : IEndableJudger
    {
        //初期化どうする問題

        int _criteriaIndex;
        int _length;

        Subject<bool> _enterabled = new Subject<bool>();
        public IObservable<bool> EnterableStateUpdated => _enterabled;

        FreeInputUnfixedText _unfixedText;

        public EnterableJudgerByLength(FreeInputUnfixedText unfixedText,int criteriaIndex)
        {
            _unfixedText = unfixedText;
            _criteriaIndex = criteriaIndex;
        }

        public bool IsEnterable()
        {
            return _length >= _criteriaIndex;
        }

        public void CatchUpdate()
        {
            bool b = IsEnterable();
            _length = _unfixedText.GetUnfixedText().Length;

            if(IsEnterable() != b)
            {
                _enterabled.OnNext(IsEnterable());
            }

        }

        public void OnExit()
        {
            Initialize();
        }

        void Initialize()
        {
            bool b = IsEnterable();
            _length = 0;
            if (b)
            {
                _enterabled.OnNext(false);
            }
            /*
            //先にUnfixedTextが初期化されていることを暗黙的に仮定していいなら、これでよかった
            CatchUpdate();
            */
        }
    }
}