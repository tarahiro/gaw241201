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
    public class ConversationViewConst
    {
        public enum EyePosition
        {
            Center,
            Top,
            RightTop,
            Right,
            RightBottom,
            Bottom,
            LeftBottom,
            Left,
            LeftTop
        }

        public enum Facial
        {
            None,
            Big,
            Mad,
        }

        public enum Impression
        {
            None,
            Silent
        }
    }
}