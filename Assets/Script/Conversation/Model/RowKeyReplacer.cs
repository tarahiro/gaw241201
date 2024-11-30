using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.DayTimeUtil;

namespace gaw241201
{
    public class RowKeyReplacer : IKeyReplacer

    {
        [Inject] IGlobalFlagProvider _flagProvider;
        public string ReplaceTo(ConversationConst.Key key)
        {
            Log.Comment("DiffSecondèëÇ´ä∑Ç¶");
            return _flagProvider.GetFlag(EnumUtil.KeyToType<FlagConst.Key>(key.ToString()));

        }
    }
}