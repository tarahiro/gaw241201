using Cysharp.Threading.Tasks;
using gaw241201.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Tarahiro;
using Tarahiro.MasterData;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.TypingUtil;

namespace gaw241201
{
    public class SelectionDataInitializer
    {
        List<IMasterDataRecord<IWordMaster>> _wordDataList;
        List<IMasterDataRecord<ILeetMaster>> _charDataList;
        [Inject] IAvailableMasterDataProvider<IMasterDataRecord<IWordMaster>> _wordMasterDataProvider;
        [Inject] IAvailableMasterDataProvider<IMasterDataRecord<ILeetMaster>> _leetMasterDataProvider;

        Subject<List<SelectionDataWithIndex>> _selectionDataInitialized = new Subject<List<SelectionDataWithIndex>>();
        public IObservable<List<SelectionDataWithIndex>> SelectionDataInitialized => _selectionDataInitialized;

        public void Initialize( string _tagSentence)
        {
            _charDataList = _leetMasterDataProvider.GetAvailableMasterDataList();
            _wordDataList = _wordMasterDataProvider.GetAvailableMasterDataList();
            var selectionDataWithindexList = new List<SelectionDataWithIndex>();

            bool _isInsideBracket = false;
            for (int i = 0; i < _tagSentence.Length;i++)
            {
                if(_tagSentence[i] == c_tagStart)
                {
                    _isInsideBracket = true;
                    if (_tagSentence.Substring(i).Contains(c_tagEnd))
                    {

                        Log.Comment("ƒ^ƒO‚ðŒŸo");
                        int index = _tagSentence.IndexOf(c_tagEnd, i) + 1;

                        if (_tagSentence[i + 1] != '/')
                        {

                            string tag = ReadTag(i, _tagSentence);
                            string substring = _tagSentence.Substring(index);


                            //word
                            string word = substring.Substring(0, substring.IndexOf(c_tagStart.ToString() + "/" + tag + c_tagEnd.ToString()));
                            foreach (var wordData in _wordDataList)
                            {
                                if (wordData.GetMaster().TagName == tag)
                                {
                                    
                                    selectionDataWithindexList.Add(new SelectionDataWithIndex(new ReplaceData(word, wordData.GetMaster().ReplaceTo), i - TypingUtil.CountCharactersInBrackets(_tagSentence,i)));
                                }
                            }
                        }
                    }
                }

                if (_tagSentence[i] == c_tagEnd)
                {
                    _isInsideBracket = false;
                }

                if (!_isInsideBracket)
                {
                    for (int _i = 0; _i < _charDataList.Count; _i++)
                    {
                        for (int j = 0; j < _charDataList[_i].GetMaster().ReplaceToStringList.Length; j++)
                        {
                            if (_tagSentence[i] == _charDataList[_i].GetMaster().ReplaceToStringList[j].ReplacedChar)
                            {
                                for (int k = 0; k < _charDataList[_i].GetMaster().ReplaceToStringList[j].StringListReplaceTo.Count; k++)
                                {
                                    selectionDataWithindexList.Add(new SelectionDataWithIndex(new ReplaceData(_charDataList[_i].GetMaster().ReplaceToStringList[j].ReplacedChar.ToString(), _charDataList[_i].GetMaster().ReplaceToStringList[j].StringListReplaceTo[k]), i - TypingUtil.CountCharactersInBrackets(_tagSentence, i)));
                                }
                            }
                        }
                    }
                }
            }

            _selectionDataInitialized.OnNext(selectionDataWithindexList);
        }
    }
}