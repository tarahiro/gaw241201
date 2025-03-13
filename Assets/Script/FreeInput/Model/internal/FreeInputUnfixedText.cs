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
        FreeInputIndexer _indexer;

        public FreeInputUnfixedText(FreeInputIndexer indexer)
        {
            _indexer = indexer;
        }


        Subject<FreeInputArgs> _updated = new Subject<FreeInputArgs>();
        public IObservable<FreeInputArgs> Updated => _updated;

        string _unfixedText;

        public void Enter(string text)
        {
            _unfixedText = text;
            Log.DebugAssert(_indexer != null);
            Log.DebugAssert(_unfixedText != null);
            _indexer.Enter(text.Length);
        }

        public void AddCharacter(char c)
        {
            _unfixedText += c;
            _indexer.TryNextFocus();
            _updated.OnNext(new FreeInputArgs(c, _unfixedText.Length - 1));
        }

        public void DeleteCharacter()
        {
            if (_unfixedText.Length > 0)
            {
                _unfixedText = _unfixedText.Substring(0, _unfixedText.Length - 1);
                _indexer.PrevFocus();
                _updated.OnNext(new FreeInputArgs(' ', _unfixedText.Length));
            }
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