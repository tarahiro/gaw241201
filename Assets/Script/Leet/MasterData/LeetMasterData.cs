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
    public class LeetMasterData : MasterDataOrderedDictionary<LeetMasterData.Record, IMasterDataRecord<ILeetMaster>>
    {
        public const string c_DataName = "Leet";

        [Serializable]
        public class Record : IMasterDataRecord<ILeetMaster>, ILeetMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] char m_LeetedChar;
            [SerializeField] string[] m_ReplaceToStringList;


            public int Index => m_Index;
            public string Id => m_Id;
            public char LeetedChar => m_LeetedChar;
            public string[] ReplaceToStringList => m_ReplaceToStringList;

            public ILeetMaster GetMaster() => this;

#if UNITY_EDITOR
            public char SettableLeetedChar { set => m_LeetedChar = value; }
            public string[] SettableReplaceToStringList { set => m_ReplaceToStringList = value; }

#endif
        }
    }
}