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
    public class FreeInputUnfixedText
    {
        [Inject] FreeInputIndexer _indexer;

        Subject<string> _updated = new Subject<string>();
        public IObservable<string> Updated => _updated;

        string _unfixedText;

        public void Enter(string text)
        {
            _unfixedText = text;
            _indexer.Enter(text.Length);
        }

        public void AddCharacter(char c)
        {
            _unfixedText += c;
            _indexer.TryNextFocus();
            _updated.OnNext(_unfixedText);
        }

        public string GetUnfixedText()
        {
            return _unfixedText;
        }

        public void Exit()
        {
            _unfixedText = "";
            _indexer.Exit();
        }
    }
}