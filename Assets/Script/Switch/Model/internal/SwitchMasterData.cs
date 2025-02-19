using Tarahiro;
using Tarahiro.MasterData;
using System;
using System.Collections;
using UnityEngine;
using gaw241201;
using static gaw241201.SwitchConst;
using System.Collections.Generic;

namespace gaw241201
{
    //---プロジェクト作成時にやること---//
    //namespaceの"project"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Switch" を置換
    //ISwitchMasterに合わせてフィールドを追加
    public class SwitchMasterData : MasterDataOrderedDictionary<SwitchMasterData.Record, IMasterDataRecord<ISwitchMaster>>
    {
        public const string c_DataName = "Switch";

        [Serializable]
        public class Record : IMasterDataRecord<ISwitchMaster>, ISwitchMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] TargetCategory m_TargetCategory;
            [SerializeField] ByCategory m_ByCategory;
            [SerializeField] string m_ByKey;
            [SerializeField] ListWrapper<ConditionAndValue> m_ConditionAndValueList;


            public int Index => m_Index;
            public string Id => m_Id;
            public TargetCategory TargetCategory => m_TargetCategory;
            public ByCategory ByCategory => m_ByCategory;
            public string ByKey => m_ByKey;
            public List<ConditionAndValue> ConditionAndValueList => m_ConditionAndValueList.GetList();

            public ISwitchMaster GetMaster() => this;

#if UNITY_EDITOR

            public TargetCategory SettableTargetCategory { set => m_TargetCategory = value; }
            public ByCategory SettableByCategory { set => m_ByCategory = value; }
            public string SettableByKey {set => m_ByKey = value; }
            public List<ConditionAndValue> SettableConditionAndValueList { set => m_ConditionAndValueList = new ListWrapper<ConditionAndValue>(value); }

#endif
        }
    }
}