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
        List<IMasterDataRecord<ILeetMaster>> _charDataList;


        public void Initialize(string tagSentence)
        {
            _charDataList = _leetMasterDataProvider.GetAvailableMasterDataList();
            _wordDataList = _wordMasterDataProvider.GetAvailableMasterDataList();
        }

        public void UpdateIndex(int targetIndex, string _tagSentence)
        {
            _index = targetIndex;

            var selectionDataList = new List<SelectionData>();

            //tag
            if (_tagSentence[_index] == c_tagStart)
            {
                Log.Comment("タグを検出");
                int index = _tagSentence.IndexOf(c_tagEnd, _index) + 1;

                if (_tagSentence[_index+ 1] != '/')
                {

                    string tag = ReadTag(_index, _tagSentence);
                    string substring = _tagSentence.Substring(index);
                    string word = substring.Substring(0, substring.IndexOf(c_tagStart.ToString() + "/" + tag + c_tagEnd.ToString()));

                    Log.DebugLog(substring);

                    //word
                    foreach (var wordData in _wordDataList)
                    {
                        if (wordData.GetMaster().TagName == tag)
                        {
                            selectionDataList.Add(new SelectionData(word, wordData.GetMaster().WordName));
                        }
                    }
                }
                _index = index;
            }

            //leet
            for (int i = 0; i < _charDataList.Count; i++)
            {
                if (_tagSentence[_index] == _charDataList[i].GetMaster().LeetedChar)
                {
                    Log.Comment("leetを検出");
                    for (int j = 0; j < _charDataList[i].GetMaster().ReplaceToStringList.Length; j++)
                    {
                        selectionDataList.Add(new SelectionData(_charDataList[i].GetMaster().LeetedChar.ToString(), _charDataList[i].GetMaster().ReplaceToStringList[j]));
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