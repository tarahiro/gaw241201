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
    public class FlowMasterData : MasterDataOrderedDictionary<FlowMasterData.Record, IMasterDataRecord<IFlowMaster>>
    {
        public const string c_DataPathPrefix = "Data/Flow/";

        [Serializable]
        public class Record : IMasterDataRecord<IFlowMaster>, IFlowMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] string m_Category;
            [SerializeField] string m_BodyId;
            [SerializeField] string m_Condition;
            [SerializeField] string m_ConditionArg;


            public int Index => m_Index;
            public string Id => m_Id;
            public string Category => m_Category;
            public string BodyId => m_BodyId;
            public string Condition => m_Condition;

            public string ConditionArg => m_ConditionArg;

            public IFlowMaster GetMaster() => this;

#if UNITY_EDITOR
            public string SettableCategory { set => m_Category = value; }
            public string SettableBodyId { set => m_BodyId = value; }
            public string SettableCondition{ set => m_Condition = value; }
            public string SettableConditionArg { set => m_ConditionArg = value; }

#endif
        }
    }
}