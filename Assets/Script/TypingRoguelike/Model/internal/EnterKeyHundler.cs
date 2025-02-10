using Cysharp.Threading.Tasks;
using gaw241201.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using Tarahiro.MasterData;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.TypingUtil;

namespace gaw241201
{
    public class EnterKeyHundler
    {
        List<char> _restrictedChar;

        [Inject] RoguelikeCorrectInputHundler _correctInputHundlable;
        [Inject] RoguelikeRestrictInputHundler _restrictInputHundlable;
        [Inject] KeyInputProcesser _keyInputProcesser;
        [Inject] IndexUpdater _indexUpdater;
        [Inject] TypedFlagRegisterer _typedFlagRegisterer;
        [Inject] ISelectionDataGettable _selectionDataGettable;
        string _tagSentence;


        Subject<Unit> _ended = new Subject<Unit>();
        public IObservable<Unit> Ended => _ended;

        public void Initialize(string tagSentence, List<char> restrictedChar)
        {
            //タグのついた文章を受け取る
            //indexを初期化する
            _tagSentence = tagSentence;

            _restrictedChar = restrictedChar;
        }

        public void EnterKey(char c)
        {
            int index = _indexUpdater.GetIndex();

            Log.Comment("index: " + index.ToString() +　"への入力が有効かを判定");
  
            if (_keyInputProcesser.TryKeyProcess(c, index, _tagSentence, _selectionDataGettable.GetSelectionData(), out var selected))
            {
                if (selected.Count > 0)
                {
                    Log.Comment("検出されたSelectedDataを処理");
                    _tagSentence = ReplaceFirstOccurrence(_tagSentence, selected[0].ReplacedString, selected[0].StringReplaceTo, index);
                }

                _indexUpdater.UpdateIndex(index + 1,_tagSentence);
                NotifyCorrectInput(c, _restrictedChar);
            }
        }

      

        public void NotifyCorrectInput(char c, List<char> restrictedChar)
        {
            int index = _indexUpdater.GetIndex();

            _typedFlagRegisterer.OnCharAdded(c);

            //入力完了処理
            bool isEndLoop;
            if (restrictedChar.Contains(c))
            {
                _restrictInputHundlable.OnCorrectInput(_tagSentence, index, out isEndLoop);
            }
            else
            {
                _correctInputHundlable.OnCorrectInput(_tagSentence, index, out isEndLoop);
            }

            if (isEndLoop)
            {
                _ended.OnNext(Unit.Default);
            }

        }

    }
}