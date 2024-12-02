using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public static class KeyInputUtil
    {
        public static bool TryGetKeyDown(out KeyCode keyCode)
        {
            foreach(var key in InputtableKeyList)
            {
                if (Input.GetKeyDown(key))
                {
                    keyCode = key;
                    return true;
                }
            }
            keyCode = KeyCode.None;
            return false;
        }


        public readonly static List<KeyCode> InputtableKeyList = new List<KeyCode>()
        {
            KeyCode.Alpha0,
            KeyCode.Alpha1,
            KeyCode.Alpha2,
            KeyCode.Alpha3,
            KeyCode.Alpha4,
            KeyCode.Alpha5,
            KeyCode.Alpha6,
            KeyCode.Alpha7,
            KeyCode.Alpha8,
            KeyCode.Alpha9,

            KeyCode.A,
            KeyCode.B,
            KeyCode.C,
            KeyCode.D,
            KeyCode.E,
            KeyCode.F,
            KeyCode.G,
            KeyCode.H,
            KeyCode.I,
            KeyCode.J,
            KeyCode.K,
            KeyCode.L,
            KeyCode.M,
            KeyCode.N,
            KeyCode.O,
            KeyCode.P,
            KeyCode.Q,
            KeyCode.R,
            KeyCode.S,
            KeyCode.T,
            KeyCode.U,
            KeyCode.V,
            KeyCode.W,
            KeyCode.X,
            KeyCode.Y,
            KeyCode.Z,
               KeyCode.Minus,
                 KeyCode.Caret,
                 KeyCode.Backslash,
                 KeyCode.At,
                 KeyCode.LeftBracket,
                 KeyCode.Semicolon,
                 KeyCode.Colon,
                 KeyCode.RightBracket,
                 KeyCode.Comma,
                 KeyCode.Period,
                 KeyCode.Slash,
                 KeyCode.Underscore,
                 KeyCode.Backspace,
                 KeyCode.Return,
                 KeyCode.Space,
        };
    }
}