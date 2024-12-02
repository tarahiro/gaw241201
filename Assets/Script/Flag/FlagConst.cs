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
    public static class FlagConst
    {
        public enum Key
        {
            ApplicationTime,
            InputTime,
            Name,
            NameLower,
            BirthDate,
        }

        public enum RegisterOrder
        {
            ReadTime
        }

        public enum MessageKey
        {

            ApplicationTime,
            DiffSecond,
            Name,
            NameLower,
        }
    }
}