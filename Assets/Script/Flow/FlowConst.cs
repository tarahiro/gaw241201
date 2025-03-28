using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public static class FlowConst
    {
        public enum Category
        {
            Conversation,
            FreeInput,
            RegisterFlag,
            DeleteUi,
            ClickInput,
            EnterEffect,
            EndEffect,
            EndGame,
            StartMonitor,
            TypingRoguelike,
            StartAct,
            SkillAchieve,
            Switch,
            NotifySave,
            GoOtherFlow,
            StartHighlight,
            RegisterKeyValuePair,
            SelectInput,
        }

    }
}