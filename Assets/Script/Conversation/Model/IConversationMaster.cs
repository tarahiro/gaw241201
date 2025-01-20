using System.Collections;
using UnityEngine;
using Tarahiro;

namespace gaw241201
{
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    //フィールドを追加
    public interface IConversationMaster : IIdentifiable, IIndexable, IGroupable
    {
        ITranslatableText Message{ get; }

        string EyePosition { get; }
        string Facial { get; }
        string Impression { get; }
    }
}