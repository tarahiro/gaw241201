using System.Collections;
using UnityEngine;
using Tarahiro;

namespace gaw241201
{
    //---プロジェクト作成時にやること---//
    //namespaceの"FakeProject"部分を変更。（gaw[yymmdd].Modelとか）

    //---クラス作成時にやること---//
    //"TypingRoguelike" を置換
    //フィールドを追加
    public interface ITypingRoguelikeMaster : IIdentifiable, IIndexable,IGroupable, IGroupListable
    {
        string[] RestrictionIdList { get; }

        float TimePerChar { get; }

        int WaveCount { get; }

        float RequiredScorePerChar { get; }

        TypingRoguelikeConst.SelectionMethod SelectionMethod { get; }

        bool IsEnableRestriction { get; }
        bool IsEnableTimeUp { get; }

        bool IsEnableWave { get; }

        bool IsEnableScore { get; }
    }
}