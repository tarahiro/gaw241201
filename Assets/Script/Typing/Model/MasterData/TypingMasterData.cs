﻿using Tarahiro;
using Tarahiro.MasterData;
using System;
using System.Collections;
using UnityEngine;
using gaw241201;
using gaw241201.Model;

namespace gaw241201.Model.MasterData
{
    //---プロジェクト作成時にやること---//
    //namespaceの"project"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    //ITemplateMasterに合わせてフィールドを追加
    public class TypingMasterData : MasterDataOrderedDictionary<TypingMasterData.Record, IMasterDataRecord<ITypingMaster>>
    {
        public const string c_DataName = "Typing";

        [Serializable]
        public class Record : IMasterDataRecord<ITypingMaster>, ITypingMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] string m_TypingGroup;
            [SerializeField] string m_JpText;
            [SerializeField] string m_RomanText;


            public int Index => m_Index;
            public string Id => m_Id;
            public string Group => m_TypingGroup;
            public string DisplayText => m_JpText;
            public string QuestionText => m_RomanText;

            public ITypingMaster GetMaster() => this;

#if UNITY_EDITOR
            public string SettableTypingGroup { set => m_TypingGroup = value; }
            public string SettableJpText { set => m_JpText = value; }
            public string SettableRomanText { set => m_RomanText = value; }
#endif
        }
    }
}