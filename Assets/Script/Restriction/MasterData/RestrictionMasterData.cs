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
    public class RestrictionMasterData : MasterDataOrderedDictionary<RestrictionMasterData.Record, IMasterDataRecord<IRestrictionMaster>>
    {
        public const string c_DataName = "Restriction";

        [Serializable]
        public class Record : IMasterDataRecord<IRestrictionMaster>, IRestrictionMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] char[] m_RestrictedCharList;


            public int Index => m_Index;
            public string Id => m_Id;
            public char[] RestrictedCharList => m_RestrictedCharList;

            public IRestrictionMaster GetMaster() => this;

#if UNITY_EDITOR
            public char[] SettableRestrictedCharList { set => m_RestrictedCharList = value; }

#endif
        }
    }
}