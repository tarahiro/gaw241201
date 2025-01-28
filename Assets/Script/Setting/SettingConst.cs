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
    public class SettingConst
    {
        public enum MenuDirection
        {
            Up = -1,
            Down = 1,
        }

        public enum TabDirection
        {
            Left = -1,
            Right = 1,
        }
    }
}