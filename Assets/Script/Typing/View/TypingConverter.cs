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
    public class TypingConverter : IKeyCodeToCharConverter
    {
        public bool TryConvertKeyCodeToChar(KeyCode key, out char c)
        {
            if (Input.inputString.Length > 0)
            {
                Log.DebugLog(Input.inputString);
                c = Input.inputString[0];
            }
            else
            {
                c = '\0';
            }

                /*

            if (KeyInputUtil.IsPressedShift())
            {

                c = GetCharFromKeyCodeWithShift(key);
            }
            else
            {
                c = GetCharFromKeyCode(key);
            }
                */

            return c != '\0';
        }

        /*
        char GetCharFromKeyCodeWithShift(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.Alpha1:
                    return '!';
                case KeyCode.Alpha2:
                    return '"';
                case KeyCode.Alpha3:
                    return '#';
                case KeyCode.Alpha4:
                    return '$';
                case KeyCode.Alpha5:
                    return '%';
                case KeyCode.Alpha6:
                    return '&';
                case KeyCode.Alpha8:
                    return '(';
                case KeyCode.Alpha9:
                    return ')';
                case KeyCode.Minus:
                    return '=';
                case KeyCode.Caret:
                    return '~';
                case KeyCode.Backslash:
                    return '|';
                case KeyCode.At:
                    return '`';
                case KeyCode.LeftBracket:
                    return '{';
                case KeyCode.Semicolon:
                    return '+';
                case KeyCode.Colon:
                    return '*';
                case KeyCode.RightBracket:
                    return '}';
                case KeyCode.Comma:
                    return '<';
                case KeyCode.Period:
                    return '>';
                case KeyCode.Slash:
                    return '?';
                case KeyCode.Underscore:
                    return '_';
                default: //上記以外のキーが押された場合は「null文字」を返す。
                    return '\0';
            }
        }

        char GetCharFromKeyCode(KeyCode keyCode)
        {
            switch (keyCode)
            {
                case KeyCode.A:
                    return 'a';
                case KeyCode.B:
                    return 'b';
                case KeyCode.C:
                    return 'c';
                case KeyCode.D:
                    return 'd';
                case KeyCode.E:
                    return 'e';
                case KeyCode.F:
                    return 'f';
                case KeyCode.G:
                    return 'g';
                case KeyCode.H:
                    return 'h';
                case KeyCode.I:
                    return 'i';
                case KeyCode.J:
                    return 'j';
                case KeyCode.K:
                    return 'k';
                case KeyCode.L:
                    return 'l';
                case KeyCode.M:
                    return 'm';
                case KeyCode.N:
                    return 'n';
                case KeyCode.O:
                    return 'o';
                case KeyCode.P:
                    return 'p';
                case KeyCode.Q:
                    return 'q';
                case KeyCode.R:
                    return 'r';
                case KeyCode.S:
                    return 's';
                case KeyCode.T:
                    return 't';
                case KeyCode.U:
                    return 'u';
                case KeyCode.V:
                    return 'v';
                case KeyCode.W:
                    return 'w';
                case KeyCode.X:
                    return 'x';
                case KeyCode.Y:
                    return 'y';
                case KeyCode.Z:
                    return 'z';
                case KeyCode.Alpha0:
                    return '0';
                case KeyCode.Alpha1:
                    return '1';
                case KeyCode.Alpha2:
                    return '2';
                case KeyCode.Alpha3:
                    return '3';
                case KeyCode.Alpha4:
                    return '4';
                case KeyCode.Alpha5:
                    return '5';
                case KeyCode.Alpha6:
                    return '6';
                case KeyCode.Alpha7:
                    return '7';
                case KeyCode.Alpha8:
                    return '8';
                case KeyCode.Alpha9:
                    return '9';
                case KeyCode.Minus:
                    return '-';
                case KeyCode.Caret:
                    return '^';
                case KeyCode.Backslash:
                    return '\\';
                case KeyCode.At:
                    return '@';
                case KeyCode.LeftBracket:
                    return '[';
                case KeyCode.Semicolon:
                    return ';';
                case KeyCode.Colon:
                    return ':';
                case KeyCode.RightBracket:
                    return ']';
                case KeyCode.Comma:
                    return ',';
                case KeyCode.Period:
                    return '.';
                case KeyCode.Slash:
                    return '/';
                case KeyCode.Underscore:
                    return '_';
                case KeyCode.Backspace:
                    return '\b';
                case KeyCode.Return:
                    return '\r';
                case KeyCode.Space:
                    return ' ';
                case KeyCode.Equals:
                    return '=';
                default: //上記以外のキーが押された場合は「null文字」を返す。
                    return '\0';
            }
        }
        */
    }
}