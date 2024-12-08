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

            //�u���v�̏_��ȓ��́iWindows�̂݁j
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

            //�u���v�̏_��ȓ��́i�uwhu�v��Windows�̂݁j
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

            //�u���v�u���v�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'c' && prevChar != 'k' &&
                currentChar == 'k' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'o'))
            {
                _questionCharList[_charIndex] = 'c';
                return true;
            }

            //�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'q' && prevChar != 'k' && currentChar == 'k' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'q';
                return true;
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'i')
            {
                _questionCharList.Insert(_charIndex, 'h');
                return true;
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'j' && currentChar == 'z' && nextChar == 'i')
            {
                _questionCharList[_charIndex] = 'j';
                return true;
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'y')
            {
                _questionCharList[_charIndex] = 'h';
                return true;
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
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

            //�u���v�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'c' && prevChar != 's' && currentChar == 's' &&
                (nextChar == 'i' || nextChar == 'e'))
            {
                _questionCharList[_charIndex] = 'c';
                return true;
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'i')
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList.Insert(_charIndex + 1, 'h');
                return true;
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'y')
            {
                _questionCharList[_charIndex] = 'c';
                return true;
            }

            //�ucya�v=>�ucha�v
            if (inputChar == 'h' && prevChar == 'c' && currentChar == 'y')
            {
                _questionCharList[_charIndex] = 'h';
                return true;
            }

            //�u�v�̏_��ȓ���
            if (inputChar == 's' && prevChar == 't' && currentChar == 'u')
            {
                _questionCharList.Insert(_charIndex, 's');
                return true;
            }

            //�u���v�u���v�u���v�u���v�̏_��ȓ���
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

            //�u�Ă��v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 't' && currentChar == 'h' && nextChar == 'i')
            {
                _questionCharList[_charIndex] = 'e';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            //�u�ł��v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'i')
            {
                _questionCharList[_charIndex] = 'e';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            //�u�ł�v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'e';
                _questionCharList.Insert(_charIndex + 1, 'x');
                _questionCharList.Insert(_charIndex + 2, 'y');
                return true;
            }

            //�u�Ƃ��v�̏_��ȓ���
            if (inputChar == 'o' && prevChar == 't' && currentChar == 'w' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'o';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }
            //�u�ǂ��v�̏_��ȓ���
            if (inputChar == 'o' && prevChar == 'd' && currentChar == 'w' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'o';
                _questionCharList.Insert(_charIndex + 1, 'x');
                return true;
            }

            //�u�Ӂv�̏_��ȓ���
            if (inputChar == 'f' && currentChar == 'h' && nextChar == 'u')
            {
                _questionCharList[_charIndex] = 'f';
                return true;
            }

            //�u�ӂ��v�u�ӂ��v�u�ӂ��v�u�ӂ��v�̏_��ȓ��́i�ꕔMac�̂݁j
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

            //�u��v�̏_��ȓ��́i�un'�v�ɂ͖��Ή��j
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

            //�u�����v�u�����v�u�����v�𕪉�����
            if (inputChar == 'u' && currentChar == 'w' && nextChar == 'h' && (nextChar2 == 'a' || nextChar2 == 'i' || nextChar2 == 'e' || nextChar2 == 'o'))
            {
                _questionCharList[_charIndex] = 'u';
                _questionCharList[_charIndex] = 'x';
            }

            //�u����v�u�ɂ�v�Ȃǂ𕪉�����
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

            //�u����v�u����v�Ȃǂ𕪉�����
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

            //�u����v���uc�v�ŕ�������iWindows����j
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

            //�u���v�̏_��ȓ���
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

            //�u�����v�u�����v�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'c' && currentChar == 'k' && nextChar == 'k' &&
                (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'o'))
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList[_charIndex + 1] = 'c';
                return true;
            }

            //�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'q' && currentChar == 'k' && nextChar == 'k' && nextChar2 == 'u')
            {
                _questionCharList[_charIndex] = 'q';
                _questionCharList[_charIndex + 1] = 'q';
                return true;
            }

            //�u�����v�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'c' && currentChar == 's' && nextChar == 's' &&
                (nextChar2 == 'i' || nextChar2 == 'e'))
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList[_charIndex + 1] = 'c';
                return true;
            }

            //�u������v�u������v�u�������v�u������v�̏_��ȓ���
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'y')
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList[_charIndex + 1] = 'c';
                return true;
            }

            //�u�����v�̏_��ȓ���
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'i')
            {
                _questionCharList[_charIndex] = 'c';
                _questionCharList[_charIndex + 1] = 'c';
                _questionCharList.Insert(_charIndex + 2, 'h');
                return true;
            }

            //�ul�v�Ɓux�v�̊��S�݊���
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