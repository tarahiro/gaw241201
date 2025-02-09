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
    public class FreeInputCharHundler
    {
        [Inject] ICharInputJudger _judger;
        [Inject] FreeInputIndexer _indexer;
        [Inject] FreeInputUnfixedText _unfixedText;


        Subject<string> _decided = new Subject<string>();
        public IObservable<string> Decided => _decided;


        public void CatchChar(char c)
        {
            if (_judger.IsCharAvailable(c))
            {
                _unfixedText.AddCharacter(c);
            }
        }

        public void Decide()
        {
            _decided.OnNext(_unfixedText.GetUnfixedText());
        }
    }
}