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
        [Inject] RoguelikeCorrectInputHundler _correctInputHundlable;
        [Inject] RoguelikeRestrictInputHundler _restrictInputHundlable;
        [Inject] KeyInputProcesser _keyInputProcesser;
        [Inject] IAvailableMasterDataProvider<IMasterDataRecord<ILeetMaster>> _leetMasterDataProvider;
        [Inject] IAvailableMasterDataProvider<IMasterDataRecord<IWordMaster>> _wordMasterDataProvider;

        List<SelectionData> selectionDataList = new List<SelectionData>();
        List<IMasterDataRecord<ILeetMaster>> _charDataList;
        List<IMasterDataRecord<IWordMaster>> _wordDataList;
        List<char> _restrictedChar = new List<char>() { 'a','c' };

        string _tagSentence;
        int _tagSentenceIndex;

        Subject<Unit> _ended = new Subject<Unit>();
        Subject<List<SelectionData>> _selectionDataCreated = new Subject<List<SelectionData>>();
        Subject<List<char>> _restrictionDataLoaded = new Subject<List<char>>();
        public IObservable<Unit> Ended => _ended;
        public IObservable<List<SelectionData>> SelectionDataCreated => _selectionDataCreated;
        public IObservable<List<char>> RestrictionDataLoaded => _restrictionDataLoaded;

        public void Initialize(string tagSentence)
        {
            //タグのついた文章を受け取る
            //indexを初期化する
            _tagSentence = tagSentence;
            _tagSentenceIndex = 0;
            _charDataList = _leetMasterDataProvider.GetAvailableMasterDataList();
            _wordDataList = _wordMasterDataProvider.GetAvailableMasterDataList();

            //Fake
            _restrictionDataLoaded.OnNext(_restrictedChar);
        }

        public void EnterKey(char c)
        {
            //入力が有効かを判定
            if(_keyInputProcesser.TryKeyProcess(c,_tagSentenceIndex, _tagSentence,selectionDataList, out var selected))
            {
                selectionDataList = new List<SelectionData>();

                if (selected.Count > 0)
                {
                    Log.Comment("検出されたSelectedDataを処理");
                    _tagSentence = ReplaceFirstOccurrence(_tagSentence, selected[0].ReplacedString, selected[0].StringReplaceTo, _tagSentenceIndex);
                }

                _tagSentenceIndex++;

                //tag
                if (_tagSentence[_tagSentenceIndex] == c_tagStart)
                {
                    int _index = _tagSentence.IndexOf(c_tagEnd, _tagSentenceIndex) + 1;

                    if (_tagSentence[_tagSentenceIndex + 1] != '/')
                    {

                        string tag = ReadTag(_tagSentenceIndex, _tagSentence);
                        string substring = _tagSentence.Substring(_index);
                        string word = substring.Substring(0, substring.IndexOf(c_tagStart.ToString() + "/" + tag + c_tagEnd.ToString()));

                        Log.DebugLog(substring);

                        foreach (var wordData in _wordDataList)
                        {
                            if (wordData.GetMaster().TagName == tag)
                            {
                                selectionDataList.Add(new SelectionData(word, wordData.GetMaster().WordName));
                            }
                        }
                    }
                    _tagSentenceIndex = _index;
                }

                //leet
                for (int i = 0; i < _charDataList.Count; i++)
                {
                    if (_tagSentence[_tagSentenceIndex] == _charDataList[i].GetMaster().LeetedChar)
                    {
                        for (int j = 0; j < _charDataList[i].GetMaster().ReplaceToStringList.Length; j++)
                        {
                            selectionDataList.Add(new SelectionData(_charDataList[i].GetMaster().LeetedChar.ToString(), _charDataList[i].GetMaster().ReplaceToStringList[j]));
                        }
                    }
                }

                //入力完了処理
                _selectionDataCreated.OnNext(selectionDataList);

                bool isEndLoop;
                if (_restrictedChar.Contains(c))
                {
                    _restrictInputHundlable.OnCorrectInput(_tagSentence, _tagSentenceIndex, out isEndLoop);
                }
                else
                {
                    _correctInputHundlable.OnCorrectInput(_tagSentence, _tagSentenceIndex, out isEndLoop);
                }

                if (isEndLoop)
                {
                    _ended.OnNext(Unit.Default);
                }

            }
        }

    }
}