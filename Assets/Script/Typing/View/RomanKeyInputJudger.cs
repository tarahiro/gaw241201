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
    public class RomanKeyInputJudger : IKeyInputJudger
    {
        [Inject] PlatformInfoProvider _platformInfoProvider;

        public bool IsKeyInputCorrect(char inputChar, int _charIndex, List<char> _questionCharList)
        {
            char prevChar3 = _charIndex >= 3 ? _questionCharList[_charIndex - 3] : '\0';
            char prevChar2 = _charIndex >= 2 ? _questionCharList[_charIndex - 2] : '\0';
            char prevChar = _charIndex >= 1 ? _questionCharList[_charIndex - 1] : '\0';
            char currentChar = _questionCharList[_charIndex];
            char nextChar = _questionCharList[_charIndex + 1];
            char nextChar2 = nextChar == '@' ? '@' : _questionCharList[_charIndex + 2];

            bool _isWindows = _platformInfoProvider.GetPlatform() == Tarahiro.Const.Platform.Windows;
            bool _isMac = _platformInfoProvider.GetPlatform() == Tarahiro.Const.Platform.Mac;

            if (inputChar == '\0')
            {
                return false;
            }

            if (inputChar == currentChar)
            {
                return true;
            }

            //「い」の柔軟な入力（Windowsのみ）
            if (_isWindows && inputChar == 'y' && currentChar == 'i' &&
                (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' || prevChar == 'u' || prevChar == 'e' ||
                 prevChar == 'o'))
            {
                _questionCharList.Insert(_charIndex, 'y');
                return true;
            }

            if (_isWindows && inputChar == 'y' && currentChar == 'i' && prevChar == 'n' && prevChar2 == 'n' &&
                prevChar3 != 'n')
            {
                _questionCharList.Insert(_charIndex, 'y');
                return true;
            }

            if (_isWindows && inputChar == 'y' && currentChar == 'i' && prevChar == 'n' && prevChar2 == 'x')
            {
                _questionCharList.Insert(_charIndex, 'y');
                return true;
            }

            //「う」の柔軟な入力（「whu」はWindowsのみ）
            if (inputChar == 'w' && currentChar == 'u' && (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' ||
                                                           prevChar == 'u' || prevChar == 'e' || prevChar == 'o'))
            {
                _questionCharList.Insert(_charIndex, 'w');
                return true;
            }

            if (inputChar == 'w' && currentChar == 'u' && prevChar == 'n' && prevChar2 == 'n' && prevChar3 != 'n')
            {
                _questionCharList.Insert(_charIndex, 'w');
                return true;
            }

            if (inputChar == 'w' && currentChar == 'u' && prevChar == 'n' && prevChar2 == 'x')
            {
                _questionCharList.Insert(_charIndex, 'w');
                return true;
            }

            if (_isWindows && inputChar == 'h' && prevChar2 != 't' && prevChar2 != 'd' && prevChar == 'w' &&
                currentChar == 'u')
            {
                _questionCharList.Insert(_charIndex, 'h');
                return true;
            }

            //「か」「く」「こ」の柔軟な入力（Windowsのみ）
            if (_isWindows && inputChar == 'c' && prevChar != 'k' &&
                currentChar == 'k' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'o'))
            {
                _questionCharList[_charIndex] = 'c';
                return true;
            }

            //「く」の柔軟な入力（Windowsのみ）
            if (_isWindows && inputChar == 'q' && prevChar != 'k' && currentChar == 'k' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'q';
                return true;
            }

            //「し」の柔軟な入力
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'i')
            {
                _questionCharList.Insert(_charIndex, 'h');
                return true;
            }

            //「じ」の柔軟な入力
            if (inputChar == 'j' && currentChar == 'z' && nextChar == 'i')
            {
                _questionCharList[_charIndex] = 'j';
                return true;
            }

            //「しゃ」「しゅ」「しぇ」「しょ」の柔軟な入力
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'y')
            {
                _questionCharList[_charIndex] = 'h';
                return true;
            }

            //「じゃ」「じゅ」「じぇ」「じょ」の柔軟な入力
            if (inputChar == 'z' && prevChar != 'j' && currentChar == 'j' &&
                (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
            {
                _questionCharList[_charIndex] = 'z';
                _questionCharList.Insert(_charIndex + 1, 'y');
                return true;
            }

            if (inputChar == 'y' && prevChar == 'j' &&
                (currentChar == 'a' || currentChar == 'u' || currentChar == 'e' || currentChar == 'o'))
            {
                _questionCharList.Insert(_charIndex, 'y');
                return true;
            }

            //「し」「せ」の柔軟な入力（Windowsのみ）
            if (_isWindows && inputChar == 'c' && prevChar != 's' && currentChar == 's' &&
                (nextChar == 'i' || nextChar == 'e'))
            {
                _questionCharList[_charIndex] = 'c';
                return true;
            }

            //「ち」の柔軟な入力
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'i')
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList.Insert(_charIndex + 1, 'h');
                return true;
            }

            //「ちゃ」「ちゅ」「ちぇ」「ちょ」の柔軟な入力
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'y')
            {
                _questionCharList[_charIndex] = 'c';
                return true;
            }

            //「cya」=>「cha」
            if (inputChar == 'h' && prevChar == 'c' && currentChar == 'y')
            {
                _questionCharList[_charIndex] = 'h';
                return true;
            }

            //「つ」の柔軟な入力
            if (inputChar == 's' && prevChar == 't' && currentChar == 'u')
            {
                _questionCharList.Insert(_charIndex, 's');
                return true;
            }

            //「つぁ」「つぃ」「つぇ」「つぉ」の柔軟な入力
            if (inputChar == 'u' && prevChar == 't' && currentChar == 's' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                _questionCharList[_charIndex] = 'u';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            if (inputChar == 'u' && prevChar2 == 't' && prevChar == 's' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                _questionCharList.Insert(_charIndex, 'u');
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            //「てぃ」の柔軟な入力
            if (inputChar == 'e' && prevChar == 't' && currentChar == 'h' && nextChar == 'i')
            {
                _questionCharList[_charIndex] = 'e';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            //「でぃ」の柔軟な入力
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'i')
            {
                _questionCharList[_charIndex] = 'e';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            //「でゅ」の柔軟な入力
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'e';
                _questionCharList.Insert(_charIndex + 1, 'x');
                _questionCharList.Insert(_charIndex + 2, 'y');
                return true;
            }

            //「とぅ」の柔軟な入力
            if (inputChar == 'o' && prevChar == 't' && currentChar == 'w' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'o';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }
            //「どぅ」の柔軟な入力
            if (inputChar == 'o' && prevChar == 'd' && currentChar == 'w' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'o';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            //「ふ」の柔軟な入力
            if (inputChar == 'f' && currentChar == 'h' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'f';
                return true;
            }

            //「ふぁ」「ふぃ」「ふぇ」「ふぉ」の柔軟な入力（一部Macのみ）
            if (inputChar == 'w' && prevChar == 'f' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                _questionCharList.Insert(_charIndex, 'w');
                return true;
            }

            if (inputChar == 'y' && prevChar == 'f' && (currentChar == 'i' || currentChar == 'e'))
            {
                _questionCharList.Insert(_charIndex, 'y');
                return true;
            }

            if (inputChar == 'h' && prevChar != 'f' && currentChar == 'f' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                if (_isMac)
                {
                    _questionCharList[_charIndex] = 'h';
                    _questionCharList.Insert(_charIndex + 1, 'w');
                }
                else
                {
                    _questionCharList[_charIndex] = 'h';
                    _questionCharList.Insert(_charIndex + 1, 'u');
                    _questionCharList.Insert(_charIndex + 2, 'x');
                }
                return true;
            }

            if (inputChar == 'u' && prevChar == 'f' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                _questionCharList.Insert(_charIndex, 'u');
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            if (_isMac && inputChar == 'u' && prevChar == 'h' && currentChar == 'w' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                _questionCharList[_charIndex] = 'u';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            //「ん」の柔軟な入力（「n'」には未対応）
            if (inputChar == 'n' && prevChar2 != 'n' && prevChar == 'n' && currentChar != 'a' && currentChar != 'i' &&
                currentChar != 'u' && currentChar != 'e' && currentChar != 'o' && currentChar != 'y')
            {
                _questionCharList.Insert(_charIndex, 'n');
                return true;
            }

            if (inputChar == 'x' && prevChar != 'n' && currentChar == 'n' && nextChar != 'a' && nextChar != 'i' &&
                nextChar != 'u' && nextChar != 'e' && nextChar != 'o' && nextChar != 'y')
            {
                if (nextChar == 'n')
                {
                    _questionCharList[_charIndex] = 'x';
                }
                else
                {
                    _questionCharList.Insert(_charIndex, 'x');
                }
                return true;
            }

            //「うぃ」「うぇ」「うぉ」を分解する
            if (inputChar == 'u' && currentChar == 'w' && nextChar == 'h' && (nextChar2 == 'a' || nextChar2 == 'i' || nextChar2 == 'e' || nextChar2 == 'o'))
            {
                _questionCharList[_charIndex] = 'u';
                _questionCharList[_charIndex] = 'x';
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
                    _questionCharList[_charIndex] = 'i';
                    _questionCharList.Insert(_charIndex + 1, 'x');
                }
                else
                {
                    _questionCharList.Insert(_charIndex, 'i');
                    _questionCharList.Insert(_charIndex + 1, 'x');
                }
                return true;
            }

            //「しゃ」「ちゃ」などを分解する
            if (inputChar == 'i' &&
                (currentChar == 'a' || currentChar == 'u' || currentChar == 'e' || currentChar == 'o') &&
                (prevChar2 == 's' || prevChar2 == 'c') && prevChar == 'h')
            {
                if (nextChar == 'e')
                {
                    _questionCharList.Insert(_charIndex, 'i');
                    _questionCharList.Insert(_charIndex + 1, 'x');
                }
                else
                {
                    _questionCharList.Insert(_charIndex, 'i');
                    _questionCharList.Insert(_charIndex + 1, 'x');
                    _questionCharList.Insert(_charIndex + 2, 'y');
                }
                return true;
            }

            //「しゃ」を「c」で分解する（Windows限定）
            if (_isWindows && inputChar == 'c' && currentChar == 's' && prevChar != 's' && nextChar == 'y' &&
                (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'e' || nextChar2 == 'o'))
            {
                if (nextChar2 == 'e')
                {
                    _questionCharList[_charIndex] = 'c';
                    _questionCharList[_charIndex + 1] = 'i';
                    _questionCharList.Insert(_charIndex + 1, 'x');
                }
                else
                {
                    _questionCharList[_charIndex] = 'c';
                    _questionCharList.Insert(_charIndex + 1, 'i');
                    _questionCharList.Insert(_charIndex + 2, 'x');
                }
                return true;
            }

            //「っ」の柔軟な入力
            if ((inputChar == 'x' || inputChar == 'l') &&
                (currentChar == 'k' && nextChar == 'k' || currentChar == 's' && nextChar == 's' ||
                 currentChar == 't' && nextChar == 't' || currentChar == 'g' && nextChar == 'g' ||
                 currentChar == 'z' && nextChar == 'z' || currentChar == 'j' && nextChar == 'j' ||
                 currentChar == 'd' && nextChar == 'd' || currentChar == 'b' && nextChar == 'b' ||
                 currentChar == 'p' && nextChar == 'p'))
            {
                _questionCharList[_charIndex] = inputChar;
                _questionCharList.Insert(_charIndex + 1, 't');
                _questionCharList.Insert(_charIndex + 2, 'u');
                return true;
            }

            //「っか」「っく」「っこ」の柔軟な入力（Windows限定）
            if (_isWindows && inputChar == 'c' && currentChar == 'k' && nextChar == 'k' &&
                (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'o'))
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList[_charIndex + 1] = 'c';
                return true;
            }

            //「っく」の柔軟な入力（Windows限定）
            if (_isWindows && inputChar == 'q' && currentChar == 'k' && nextChar == 'k' && nextChar2 == 'u')
            {
                _questionCharList[_charIndex] = 'q';
                _questionCharList[_charIndex + 1] = 'q';
                return true;
            }

            //「っし」「っせ」の柔軟な入力（Windows限定）
            if (_isWindows && inputChar == 'c' && currentChar == 's' && nextChar == 's' &&
                (nextChar2 == 'i' || nextChar2 == 'e'))
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList[_charIndex + 1] = 'c';
                return true;
            }

            //「っちゃ」「っちゅ」「っちぇ」「っちょ」の柔軟な入力
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'y')
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList[_charIndex + 1] = 'c';
                return true;
            }

            //「っち」の柔軟な入力
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'i')
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList[_charIndex + 1] = 'c';
                _questionCharList.Insert(_charIndex + 2, 'h');
                return true;
            }

            //「l」と「x」の完全互換性
            if (inputChar == 'x' && currentChar == 'l')
            {
                _questionCharList[_charIndex] = 'x';
                return true;
            }

            if (inputChar == 'l' && currentChar == 'x')
            {
                _questionCharList[_charIndex] = 'l';
                return true;
            }

            return false;

        }
    }
}