﻿using System.Collections;
using UnityEngine;
using Tarahiro;

namespace gaw241201.Model
{
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"Template" を置換
    //フィールドを追加
    public interface ITypingMaster : IIdentifiable, IIndexable, IGroupable
    {

        /// <summary>
        /// このデータのIDを取得します。
        /// </summary>

        TranslatableText DisplayText { get; }
        TranslatableText QuestionText { get; }
    }
}