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
        [Inject] IAvailableMasterDataProvider<IMasterDataRecord<IWordMaster>> _wordMasterDataProvider;

        Subject<List<SelectionDataWithIndex>> _selectionDataInitialized = new Subject<List<SelectionDataWithIndex>>();
        public IObservable<List<SelectionDataWithIndex>> SelectionDataInitialized => _selectionDataInitialized;

        public void Initialize( string _tagSentence)
        {
            _wordDataList = _wordMasterDataProvider.GetAvailableMasterDataList();
            var selectionDataWithindexList = new List<SelectionDataWithIndex>();
            for (int i = 0; i < _tagSentence.Length;i++)
            {
                if(_tagSentence[i] == c_tagStart)
                {
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
            }

            _selectionDataInitialized.OnNext(selectionDataWithindexList);
        }
    }
}