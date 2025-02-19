using System.Collections;
using UnityEngine;
using Tarahiro;
using static gaw241201.SwitchConst;
using System.Collections.Generic;

namespace gaw241201
{
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    //フィールドを追加
    public interface ISwitchMaster : IIdentifiable, IIndexable
    {
        TargetCategory TargetCategory { get; }
        ByCategory ByCategory { get; }

        string ByKey { get; }
        List<ConditionAndValue> ConditionAndValueList { get; }

    }
}