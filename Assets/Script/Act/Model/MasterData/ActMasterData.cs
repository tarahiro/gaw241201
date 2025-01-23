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
    public class ActMasterData : MasterDataOrderedDictionary<ActMasterData.Record, IMasterDataRecord<IActMaster>>
    {
        public const string c_DataName = "Act";

        [Serializable]
        public class Record : IMasterDataRecord<IActMaster>, IActMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] string m_BgId;
            [SerializeField] string m_StageGroupId;


            public int Index => m_Index;
            public string Id => m_Id;
            public string BgId => m_BgId;
            public string StageGroupId => m_StageGroupId;

            public IActMaster GetMaster() => this;

#if UNITY_EDITOR
            public string SettableBgId { set => m_BgId = value; }
            public string SettableStageGroupId { set => m_StageGroupId = value; }

#endif
        }
    }
}