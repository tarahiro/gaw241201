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
    public class ConversationMasterData : MasterDataOrderedDictionary<ConversationMasterData.Record, IMasterDataRecord<IConversationMaster>>
    {
        public const string c_DataName = "Conversation";

        [Serializable]
        public class Record : IMasterDataRecord<IConversationMaster>, IConversationMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] string m_ConversationGroup;
            [SerializeField] string m_Message;
            [SerializeField] string m_EyePosition;
            [SerializeField] string m_Facial;
            [SerializeField] string m_Impression;


            public int Index => m_Index;
            public string Id => m_Id;
            public string Group => m_ConversationGroup;
            public string Message => m_Message;
            public string EyePosition => m_EyePosition;
            public string Facial => m_Facial;
            public string Impression => m_Impression;

            public IConversationMaster GetMaster() => this;

#if UNITY_EDITOR
            public string SettableConversationGroup { set => m_ConversationGroup = value; }
            public string SettableMessage { set => m_Message = value; }
            public string SettableEyePosition { set => m_EyePosition = value; }
            public string SettableFacial { set => m_Facial = value; }
            public string SettableImpression { set => m_Impression = value; }

#endif
        }
    }
}