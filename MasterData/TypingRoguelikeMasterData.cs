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
    public class TypingRoguelikeMasterData : MasterDataOrderedDictionary<TypingRoguelikeMasterData.Record, IMasterDataRecord<ITypingRoguelikeMaster>>
    {
        public const string c_DataName = "TypingRoguelike";

        [Serializable]
        public class Record : IMasterDataRecord<ITypingRoguelikeMaster>, ITypingRoguelikeMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] string m_Group;
            [SerializeField] string[] m_GroupList;
            [SerializeField] string[] m_RestrictionId;
            [SerializeField] float m_TimePerChar;
            [SerializeField] int m_WaveCount;
            [SerializeField] float m_RequiredScorePerChar;

            public int Index => m_Index;
            public string Id => m_Id;
            public string Group => m_Group;
            public string[] GroupList => m_GroupList;
            public string[] RestrictionIdList => m_RestrictionId;
            public float TimePerChar => m_TimePerChar;
            public int WaveCount => m_WaveCount;
            public float RequiredScorePerChar => m_RequiredScorePerChar;

            public ITypingRoguelikeMaster GetMaster() => this;

#if UNITY_EDITOR
            public string SettableGroup { set => m_Group = value; }
            public string[] SettableGroupList { set => m_GroupList = value; }
            public string[] SettableRestrictionId { set => m_RestrictionId = value; }
            public float SettableTimePerChar { set => m_TimePerChar = value; }
            public int SettableWaveCount { set => m_WaveCount = value; }
            public float SettableRequiredScorePerChar { set => m_RequiredScorePerChar = value; }

#endif
        }
    }
}