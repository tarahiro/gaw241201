using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201
{
    public static class EffectConst
    {
        public enum Key
        {
            GlitchLargeAutoEnd,
            GlitchSmall,
            GlitchLarge,
            CmdError,
            CmdAnswer,
            CmdRm,
            Wait,
            ConfiscateLeftEye,
            ConfiscateBothEye,
            SetGoatEye,
            ChangeEyesPositionToMiddleUp,
            ChangeEyesPositionToMiddleDown,
            GlitchMiddleAutoEnd,
            SetNormalEye,
        }

        public enum EyeParts
        {
            Normal,
            Goat,
            Real,
        }

        public enum WhichEye
        {
            Both,
            Left,
            Right
        }
    }
}