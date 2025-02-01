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
    public class TypingMasterData : MasterDataOrderedDictionary<TypingMasterData.Record, IMasterDataRecord<ITypingMaster>>
    {
        public const string c_DataName = "Typing";

        [Serializable]
        public class Record : IMasterDataRecord<ITypingMaster>, ITypingMaster
        {
            public Record(int index, string id)
            {
                m_Index = index;
                m_Id = id;
            }

            [SerializeField] int m_Index;
            [SerializeField] string m_Id;
            [SerializeField] string m_TypingGroup;
            [SerializeField] TranslatableText m_DisplayText;
            [SerializeField] TranslatableText m_QuestionText;


            public int Index => m_Index;
            public string Id => m_Id;
            public string Group => m_TypingGroup;
            public TranslatableText DisplayText => m_DisplayText;
            public TranslatableText QuestionText => m_QuestionText;

            public ITypingMaster GetMaster() => this;

#if UNITY_EDITOR
            public string SettableTypingGroup { set => m_TypingGroup = value; }
            public TranslatableText SettableDisplayText { set => m_DisplayText = value; }
            public TranslatableText SettableQuestionText  { set => m_QuestionText = value; }
#endif
        }
    }
}