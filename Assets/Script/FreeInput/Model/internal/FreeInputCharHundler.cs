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
    public class FreeInputCharHundler : IFreeInputCharHundler
    {
        ICharJudger _judger;
        FreeInputUnfixedText _unfixedText;

        public FreeInputCharHundler(ICharJudger judger, FreeInputUnfixedText unfixedText)
        {
            _judger = judger;
            _unfixedText = unfixedText;
        }



        Subject<string> _ended = new Subject<string>();
        public IObservable<string> Ended => _ended;


        public void CatchChar(char c)
        {
            if (_judger.IsCharAvailable(c))
            {
                _unfixedText.AddCharacter(c);
            }
        }

        public void TryEnd()
        {
            _ended.OnNext(_unfixedText.GetUnfixedText());
        }

        public void Delete()
        {
            _unfixedText.DeleteCharacter();
        }
    }
}