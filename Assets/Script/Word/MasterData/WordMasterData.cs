using Tarahiro;
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
    public class WordMasterData : MasterDataOrderedDictionary<WordMasterData.Record, IMasterDataRecord<IWordMaster>>
    {
        public const string c_DataName = "Word";

        [Serializable]
        public class Record : IMasterDataRecord<IWordMaster>, IWordMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] TranslatableText m_DisplayName;
            [SerializeField] TranslatableText m_Description;
            [SerializeField] string m_ReplaceTo;
            [SerializeField] string m_TagName;
            [SerializeField] string m_SkillKey;
            [SerializeField] string[] m_SkillStringArgs;
            [SerializeField] float m_SkillFloatArg;

            public int Index => m_Index;
            public string Id => m_Id;
            public TranslatableText DisplayName => m_DisplayName;
            public TranslatableText Description => m_Description;
            public string ReplaceTo => m_ReplaceTo;
            public string TagName => m_TagName;
            public string SkillKey => m_SkillKey;
            public string[] SkillStringArgs => m_SkillStringArgs;
            public float SkillFloatArg => m_SkillFloatArg;

            public IWordMaster GetMaster() => this;

#if UNITY_EDITOR
            public TranslatableText SettableDisplayName { set => m_DisplayName = value; }
            public TranslatableText SettableDescription { set => m_Description = value; }
            public string SettableReplaceTo { set => m_ReplaceTo = value; }
            public string SettableTagName { set => m_TagName = value; }
            public string SettableSkillKey { set => m_SkillKey = value; }
            public string[] SettableSkillStringArgs { set => m_SkillStringArgs = value; }
            public float SettableSkillFloatArg { set => m_SkillFloatArg = value; }

#endif
        }
    }
}