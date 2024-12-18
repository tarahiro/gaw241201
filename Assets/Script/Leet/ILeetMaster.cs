using System.Collections;
using UnityEngine;
using Tarahiro;
using UnityEditor;

namespace gaw241201.Model
{
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    //フィールドを追加
    public interface ILeetMaster : IIdentifiable, IIndexable
    {
        string Name { get; }
        string Description { get; }
        LeetReplaceData[] ReplaceToStringList { get; }
    }
}