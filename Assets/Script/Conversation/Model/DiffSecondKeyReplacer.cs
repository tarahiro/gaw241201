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
    public class DiffSecondKeyReplacer : IKeyReplacer
    {
        [Inject] IGlobalFlagProvider _flagProvider;
        public string ReplaceTo(ConversationConst.Key key)
        {
            Log.Comment("DiffSecondèëÇ´ä∑Ç¶");

            TimeInDay applicationTid = CreateTimeInDay(_flagProvider.GetFlag(FlagConst.Key.ApplicationTime));
            TimeInDay inputTid = CreateTimeInDay(_flagProvider.GetFlag(FlagConst.Key.InputTime));

            int diff = Mathf.Abs(applicationTid.GetAllSecond() - inputTid.GetAllSecond());

            return diff.ToString();
        }
    }
}