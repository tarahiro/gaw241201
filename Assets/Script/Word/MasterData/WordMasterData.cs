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
            [SerializeField] string m_WordName;
            [SerializeField] string m_TagName;
            [SerializeField] string m_Description;
            [SerializeField] string m_SkillKey;
            [SerializeField] string[] m_SkillStringArgs;
            [SerializeField] float m_SkillFloatArg;

            public int Index => m_Index;
            public string Id => m_Id;
            public string WordName => m_WordName;
            public string TagName => m_TagName;
            public string Description => m_Description;
            public string SkillKey => m_SkillKey;
            public string[] SkillStringArgs => m_SkillStringArgs;
            public float SkillFloatArg => m_SkillFloatArg;

            public IWordMaster GetMaster() => this;

#if UNITY_EDITOR
            public string SettableWordName { set => m_WordName = value; }
            public string SettableTagName { set => m_TagName = value; }
            public string SettableDescription { set => m_Description = value; }
            public string SettableSkillKey { set => m_SkillKey = value; }
            public string[] SettableSkillStringArgs { set => m_SkillStringArgs = value; }
            public float SettableSkillFloatArg { set => m_SkillFloatArg = value; }

#endif
        }
    }
}