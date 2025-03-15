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
    public class EnterableJudgerByIndex : IEndableJudger
    {
        //èâä˙âªÇ«Ç§Ç∑ÇÈñ‚ëË

        int _criteriaIndex;
        int _index;

        Subject<bool> _enterabled = new Subject<bool>();
        public IObservable<bool> EnterableStateUpdated => _enterabled;

        public EnterableJudgerByIndex(int criteriaIndex)
        {
            _criteriaIndex = criteriaIndex;
        }

        public bool IsEnterable()
        {
            return _index >= _criteriaIndex;
        }

        public void CatchUpdateFocus(int index)
        {
            bool b = IsEnterable();
            _index = index;

            if(IsEnterable() != b)
            {
                _enterabled.OnNext(IsEnterable());
            }

        }
    }
}