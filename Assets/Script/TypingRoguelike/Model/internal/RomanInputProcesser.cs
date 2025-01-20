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
    public class RomanInputProcesser
    {
        [Inject] PlatformInfoProvider _platformInfoProvider;


        //別の文字が来た時に、ローマ字入力的に正しければ、そのReplaceDataを返す
        public ReplaceData IsKeyInputCorrect(char inputChar, int _charIndex, string questionString)
        {
            Log.DebugAssert(_charIndex < questionString.Length);

            char prevChar3 = _charIndex >= 3 ? questionString[_charIndex - 3] : '\0';
            char prevChar2 = _charIndex >= 2 ? questionString[_charIndex - 2] : '\0';
            char prevChar = _charIndex >= 1 ? questionString[_charIndex - 1] : '\0';
            char currentChar = questionString[_charIndex];
            char nextChar = questionString[_charIndex + 1];
            char nextChar2 = nextChar == '@' ? '@' : questionString[_charIndex + 2];

            bool _isWindows = _platformInfoProvider.GetPlatform() == Tarahiro.Const.Platform.Windows;
            bool _isMac = _platformInfoProvider.GetPlatform() == Tarahiro.Const.Platform.Mac;

            if (inputChar == '\0')
            {
                return null;
            }

            if (inputChar == currentChar)
            {
                return null;
            }

            //「い」の柔軟な入力（Windowsのみ）
            if (_isWindows && inputChar == 'y' && currentChar == 'i' &&
                (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' || prevChar == 'u' || prevChar == 'e' ||
                 prevChar == 'o'))
            {
                return new ReplaceData("i", "yi");
            }

            if (_isWindows && inputChar == 'y' && currentChar == 'i' && prevChar == 'n' && prevChar2 == 'n' &&
                prevChar3 != 'n')
            {
                return new ReplaceData("i", "yi");
            }

            if (_isWindows && inputChar == 'y' && currentChar == 'i' && prevChar == 'n' && prevChar2 == 'x')
            {
                return new ReplaceData("i", "yi");
            }

            //「う」の柔軟な入力（「whu」はWindowsのみ）
            if (inputChar == 'w' && currentChar == 'u' && (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' ||
                                                           prevChar == 'u' || prevChar == 'e' || prevChar == 'o'))
            {
                return new ReplaceData("u", "wu");
            }

            if (inputChar == 'w' && currentChar == 'u' && prevChar == 'n' && prevChar2 == 'n' && prevChar3 != 'n')
            {
                return new ReplaceData("u", "wu");
            }

            if (inputChar == 'w' && currentChar == 'u' && prevChar == 'n' && prevChar2 == 'x')
            {
                return new ReplaceData("u", "wu");
            }

            if (_isWindows && inputChar == 'h' && prevChar2 != 't' && prevChar2 != 'd' && prevChar == 'w' &&
                currentChar == 'u')
            {
                return new ReplaceData("u", "hu");
            }

            //「か」「く」「こ」の柔軟な入力（Windowsのみ）
            if (_isWindows && inputChar == 'c' && prevChar != 'k' &&
                currentChar == 'k' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'o'))
            {
                return new ReplaceData("k", "c");
            }

            //「く」の柔軟な入力（Windowsのみ）
            if (_isWindows && inputChar == 'q' && prevChar != 'k' && currentChar == 'k' && nextChar == 'u')
            {
                return new ReplaceData("k", "q");
            }

            //「し」の柔軟な入力
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'i')
            {
                return new ReplaceData("i", "hi");
            }

            //「じ」の柔軟な入力
            if (inputChar == 'j' && currentChar == 'z' && nextChar == 'i')
            {
                return new ReplaceData("z", "j");
            }

            //「しゃ」「しゅ」「しぇ」「しょ」の柔軟な入力
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'y')
            {
                return new ReplaceData("y", "h");
            }

            //「じゃ」「じゅ」「じぇ」「じょ」の柔軟な入力
            if (inputChar == 'z' && prevChar != 'j' && currentChar == 'j' &&
                (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
            {
                return new ReplaceData("j", "zy");
            }

            if (inputChar == 'y' && prevChar == 'j' &&
                (currentChar == 'a' || currentChar == 'u' || currentChar == 'e' || currentChar == 'o'))
            {
                return new ReplaceData(currentChar.ToString(), string.Concat('y', currentChar));
            }

            //「し」「せ」の柔軟な入力（Windowsのみ）
            if (_isWindows && inputChar == 'c' && prevChar != 's' && currentChar == 's' &&
                (nextChar == 'i' || nextChar == 'e'))
            {
                return new ReplaceData("s", "c");
            }

            //「ち」の柔軟な入力
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'i')
            {
                return new ReplaceData("t", "ch");
            }

            //「ちゃ」「ちゅ」「ちぇ」「ちょ」の柔軟な入力
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'y')
            {
                return new ReplaceData("t", "c");
            }

            //「cya」=>「cha」
            if (inputChar == 'h' && prevChar == 'c' && currentChar == 'y')
            {
                return new ReplaceData("y", "h");
            }

            //「つ」の柔軟な入力
            if (inputChar == 's' && prevChar == 't' && currentChar == 'u')
            {
                return new ReplaceData("u", "su");
            }

            //「つぁ」「つぃ」「つぇ」「つぉ」の柔軟な入力
            if (inputChar == 'u' && prevChar == 't' && currentChar == 's' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                return new ReplaceData("s", "ux");

            }

            if (inputChar == 'u' && prevChar2 == 't' && prevChar == 's' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                return new ReplaceData(currentChar.ToString(),string.Concat('u','x',currentChar));
            }

            //「てぃ」の柔軟な入力
            if (inputChar == 'e' && prevChar == 't' && currentChar == 'h' && nextChar == 'i')
            {
                return new ReplaceData("h", "exi");
            }

            //「でぃ」の柔軟な入力
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'i')
            {
                return new ReplaceData("h", "exi");
            }

            //「でゅ」の柔軟な入力
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'u')
            {
                return new ReplaceData("h", "exy");
            }

            //「とぅ」の柔軟な入力
            if (inputChar == 'o' && prevChar == 't' && currentChar == 'w' && nextChar == 'u')
            {
                return new ReplaceData("w", "oxu");
            }
            //「どぅ」の柔軟な入力
            if (inputChar == 'o' && prevChar == 'd' && currentChar == 'w' && nextChar == 'u')
            {
                return new ReplaceData("w", "oxu");
            }

            //「ふ」の柔軟な入力
            if (inputChar == 'f' && currentChar == 'h' && nextChar == 'u')
            {
                return new ReplaceData("h", "f");
            }

            //「ふぁ」「ふぃ」「ふぇ」「ふぉ」の柔軟な入力（一部Macのみ）
            if (inputChar == 'w' && prevChar == 'f' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                return new ReplaceData(currentChar.ToString(), string.Concat('w',currentChar));
            }

            if (inputChar == 'y' && prevChar == 'f' && (currentChar == 'i' || currentChar == 'e'))
            {
                return new ReplaceData(currentChar.ToString(), string.Concat('y', currentChar));
            }

            if (inputChar == 'h' && prevChar != 'f' && currentChar == 'f' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                if (_isMac)
                {
                    return new ReplaceData("f", "hw");
                }
                else
                {
                    return new ReplaceData("f", "hux");
                }
            }

            if (inputChar == 'u' && prevChar == 'f' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                return new ReplaceData(currentChar.ToString(), string.Concat('u','x', currentChar));

            }

            if (_isMac && inputChar == 'u' && prevChar == 'h' && currentChar == 'w' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                return new ReplaceData("w", "ux");
            }

            //「ん」の柔軟な入力（「n'」には未対応）
            if (inputChar == 'n' && prevChar2 != 'n' && prevChar == 'n' && currentChar != 'a' && currentChar != 'i' &&
                currentChar != 'u' && currentChar != 'e' && currentChar != 'o' && currentChar != 'y')
            {
                return new ReplaceData(currentChar.ToString(), string.Concat('n',currentChar));
            }

            if (inputChar == 'x' && prevChar != 'n' && currentChar == 'n' && nextChar != 'a' && nextChar != 'i' &&
                nextChar != 'u' && nextChar != 'e' && nextChar != 'o' && nextChar != 'y')
            {
                if (nextChar == 'n')
                {
                    return new ReplaceData("n","x");
                }
                else
                {
                    return new ReplaceData("n", "xn");
                }
            }

            //「うぃ」「うぇ」「うぉ」を分解する
            if (inputChar == 'u' && currentChar == 'w' && nextChar == 'h' && (nextChar2 == 'a' || nextChar2 == 'i' || nextChar2 == 'e' || nextChar2 == 'o'))
            {
                return new ReplaceData("wh", "ux");
            }

            //「きゃ」「にゃ」などを分解する
            if (inputChar == 'i' && currentChar == 'y' &&
                (prevChar == 'k' || prevChar == 's' || prevChar == 't' || prevChar == 'n' || prevChar == 'h' ||
                 prevChar == 'm' || prevChar == 'r' || prevChar == 'g' || prevChar == 'z' || prevChar == 'd' ||
                 prevChar == 'b' || prevChar == 'p') &&
                (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
            {
                if (nextChar == 'e')
                {
                    return new ReplaceData("y", "ix");
                }
                else
                {
                    return new ReplaceData("y", "ixy");
                }
            }

            //「しゃ」「ちゃ」などを分解する
            if (inputChar == 'i' &&
                (currentChar == 'a' || currentChar == 'u' || currentChar == 'e' || currentChar == 'o') &&
                (prevChar2 == 's' || prevChar2 == 'c') && prevChar == 'h')
            {
                if (nextChar == 'e')
                {
                    return new ReplaceData(currentChar.ToString(), "ix");
                }
                else
                {
                    return new ReplaceData(currentChar.ToString(), "ixy");
                }
            }

            //「しゃ」を「c」で分解する（Windows限定）
            if (_isWindows && inputChar == 'c' && currentChar == 's' && prevChar != 's' && nextChar == 'y' &&
                (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'e' || nextChar2 == 'o'))
            {
                if (nextChar2 == 'e')
                {
                    return new ReplaceData("sy", "cix");
                }
                else
                {
                    return new ReplaceData("sy", "cixy");
                }
            }

            //「っ」の柔軟な入力
            if ((inputChar == 'x' || inputChar == 'l') &&
                (currentChar == 'k' && nextChar == 'k' || currentChar == 's' && nextChar == 's' ||
                 currentChar == 't' && nextChar == 't' || currentChar == 'g' && nextChar == 'g' ||
                 currentChar == 'z' && nextChar == 'z' || currentChar == 'j' && nextChar == 'j' ||
                 currentChar == 'd' && nextChar == 'd' || currentChar == 'b' && nextChar == 'b' ||
                 currentChar == 'p' && nextChar == 'p'))
            {
                return new ReplaceData(currentChar.ToString(), string.Concat(inputChar,'t','u'));
            }

            //「っか」「っく」「っこ」の柔軟な入力（Windows限定）
            if (_isWindows && inputChar == 'c' && currentChar == 'k' && nextChar == 'k' &&
                (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'o'))
            {
                return new ReplaceData("kk","cc");
            }

            //「っく」の柔軟な入力（Windows限定）
            if (_isWindows && inputChar == 'q' && currentChar == 'k' && nextChar == 'k' && nextChar2 == 'u')
            {
                return new ReplaceData("kk", "qq");
            }

            //「っし」「っせ」の柔軟な入力（Windows限定）
            if (_isWindows && inputChar == 'c' && currentChar == 's' && nextChar == 's' &&
                (nextChar2 == 'i' || nextChar2 == 'e'))
            {
                return new ReplaceData("ss", "cc");
            }

            //「っちゃ」「っちゅ」「っちぇ」「っちょ」の柔軟な入力
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'y')
            {
                return new ReplaceData("tt", "cc");
            }

            //「っち」の柔軟な入力
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'i')
            {
                return new ReplaceData("tt", "cch");
            }

            //「l」と「x」の完全互換性
            if (inputChar == 'x' && currentChar == 'l')
            {
                return new ReplaceData("l", "x");
            }

            if (inputChar == 'l' && currentChar == 'x')
            {
                return new ReplaceData("x", "l");
            }


            return null;

        }
    }
}