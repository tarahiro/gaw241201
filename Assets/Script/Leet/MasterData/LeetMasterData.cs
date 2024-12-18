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
            [SerializeField] string m_Name;
            [SerializeField] string m_Description;
            [SerializeField] LeetReplaceData[] m_ReplaceToStringList;


            public int Index => m_Index;
            public string Id => m_Id;
            public string Name => m_Name;
            public string Description => m_Description;
            public LeetReplaceData[] ReplaceToStringList => m_ReplaceToStringList;

            public ILeetMaster GetMaster() => this;

#if UNITY_EDITOR
            public string SettableName { set => m_Name = value; }
            public string SettableDescription { set => m_Description = value; }
            public LeetReplaceData[] SettableReplaceToStringList { set => m_ReplaceToStringList = value; }

#endif
        }
    }
}