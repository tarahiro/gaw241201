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
    public class IndexUpdater
    {
        int _index;
        List<IMasterDataRecord<IWordMaster>> _wordDataList;
        [Inject] IAvailableMasterDataProvider<IMasterDataRecord<ILeetMaster>> _leetMasterDataProvider;
        [Inject] IAvailableMasterDataProvider<IMasterDataRecord<IWordMaster>> _wordMasterDataProvider;
        [Inject] ISelectionDataSettable _selectionDataSettable;
        [Inject] TypedFlagRegisterer _typedFlagRegisterer;

        List<IMasterDataRecord<ILeetMaster>> _charDataList;


        public void Initialize(string tagSentence)
        {
            _charDataList = _leetMasterDataProvider.GetAvailableMasterDataList();
            _wordDataList = _wordMasterDataProvider.GetAvailableMasterDataList();
        }

        public void UpdateIndex(int targetIndex, string _tagSentence)
        {
            _index = targetIndex;

            var selectionDataList = new List<ReplaceData>();

            //tag
            while (_tagSentence[_index] == c_tagStart)
            {
                if (_tagSentence.Substring(_index).Contains(c_tagEnd))
                {

                    Log.Comment("É^ÉOÇåüèo");
                    int index = _tagSentence.IndexOf(c_tagEnd, _index) + 1;

                    if (_tagSentence[_index + 1] != '/')
                    {

                        string tag = ReadTag(_index, _tagSentence);
                        string substring = _tagSentence.Substring(index);


                        //word
                        string word = substring.Substring(0, substring.IndexOf(c_tagStart.ToString() + "/" + tag + c_tagEnd.ToString()));
                        foreach (var wordData in _wordDataList)
                        {
                            if (wordData.GetMaster().TagName == tag)
                            {
                                selectionDataList.Add(new ReplaceData(word, wordData.GetMaster().ReplaceTo));
                            }
                        }

                        //register
                        if (tag.StartsWith("fla:"))
                        {
                            string subtag = tag.Replace("fla:", "");
                            _typedFlagRegisterer.StartRegister(subtag);
                            Log.Comment("ìoò^äJén");
                        }

                    }
                    else
                    {
                        string tag = ReadTag(_index, _tagSentence);
                        //register
                        if (tag.StartsWith("/fla:"))
                        {
                            string subtag = tag.Replace("/fla:", "");
                            _typedFlagRegisterer.EndRegister(subtag);
                            Log.Comment("ìoò^èIóπ");
                        }

                    }
                    _index = index;
                }
            }

            //leet
            for (int i = 0; i < _charDataList.Count; i++)
            {
                for (int j = 0; j < _charDataList[i].GetMaster().ReplaceToStringList.Length; j++)
                {
                    if (_tagSentence[_index] == _charDataList[i].GetMaster().ReplaceToStringList[j].ReplacedChar)
                    {
                        Log.Comment("leetÇåüèo");
                        for (int k = 0; k < _charDataList[i].GetMaster().ReplaceToStringList[j].StringListReplaceTo.Count; k++)
                        {
                            selectionDataList.Add(new ReplaceData(_charDataList[i].GetMaster().ReplaceToStringList[j].ReplacedChar.ToString(), _charDataList[i].GetMaster().ReplaceToStringList[j].StringListReplaceTo[k]));
                        }
                    }
                }
            }

            _selectionDataSettable.SetSelectionData(selectionDataList);
        }

        public int GetIndex()
        {
            return _index;
        }
    }
}