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


        //�ʂ̕������������ɁA���[�}�����͓I�ɐ�������΁A����ReplaceData��Ԃ�
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

            //�u���v�̏_��ȓ��́iWindows�̂݁j
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

            //�u���v�̏_��ȓ��́i�uwhu�v��Windows�̂݁j
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

            //�u���v�u���v�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'c' && prevChar != 'k' &&
                currentChar == 'k' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'o'))
            {
                return new ReplaceData("k", "c");
            }

            //�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'q' && prevChar != 'k' && currentChar == 'k' && nextChar == 'u')
            {
                return new ReplaceData("k", "q");
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'i')
            {
                return new ReplaceData("i", "hi");
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'j' && currentChar == 'z' && nextChar == 'i')
            {
                return new ReplaceData("z", "j");
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'y')
            {
                return new ReplaceData("y", "h");
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
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

            //�u���v�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'c' && prevChar != 's' && currentChar == 's' &&
                (nextChar == 'i' || nextChar == 'e'))
            {
                return new ReplaceData("s", "c");
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'i')
            {
                return new ReplaceData("t", "ch");
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'y')
            {
                return new ReplaceData("t", "c");
            }

            //�ucya�v=>�ucha�v
            if (inputChar == 'h' && prevChar == 'c' && currentChar == 'y')
            {
                return new ReplaceData("y", "h");
            }

            //�u�v�̏_��ȓ���
            if (inputChar == 's' && prevChar == 't' && currentChar == 'u')
            {
                return new ReplaceData("u", "su");
            }

            //�u���v�u���v�u���v�u���v�̏_��ȓ���
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

            //�u�Ă��v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 't' && currentChar == 'h' && nextChar == 'i')
            {
                return new ReplaceData("h", "exi");
            }

            //�u�ł��v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'i')
            {
                return new ReplaceData("h", "exi");
            }

            //�u�ł�v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'u')
            {
                return new ReplaceData("h", "exy");
            }

            //�u�Ƃ��v�̏_��ȓ���
            if (inputChar == 'o' && prevChar == 't' && currentChar == 'w' && nextChar == 'u')
            {
                return new ReplaceData("w", "oxu");
            }
            //�u�ǂ��v�̏_��ȓ���
            if (inputChar == 'o' && prevChar == 'd' && currentChar == 'w' && nextChar == 'u')
            {
                return new ReplaceData("w", "oxu");
            }

            //�u�Ӂv�̏_��ȓ���
            if (inputChar == 'f' && currentChar == 'h' && nextChar == 'u')
            {
                return new ReplaceData("h", "f");
            }

            //�u�ӂ��v�u�ӂ��v�u�ӂ��v�u�ӂ��v�̏_��ȓ��́i�ꕔMac�̂݁j
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

            //�u��v�̏_��ȓ��́i�un'�v�ɂ͖��Ή��j
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

            //�u�����v�u�����v�u�����v�𕪉�����
            if (inputChar == 'u' && currentChar == 'w' && nextChar == 'h' && (nextChar2 == 'a' || nextChar2 == 'i' || nextChar2 == 'e' || nextChar2 == 'o'))
            {
                return new ReplaceData("wh", "ux");
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
                    return new ReplaceData("y", "ix");
                }
                else
                {
                    return new ReplaceData("y", "ixy");
                }
            }

            //�u����v�u����v�Ȃǂ𕪉�����
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

            //�u����v���uc�v�ŕ�������iWindows����j
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

            //�u���v�̏_��ȓ���
            if ((inputChar == 'x' || inputChar == 'l') &&
                (currentChar == 'k' && nextChar == 'k' || currentChar == 's' && nextChar == 's' ||
                 currentChar == 't' && nextChar == 't' || currentChar == 'g' && nextChar == 'g' ||
                 currentChar == 'z' && nextChar == 'z' || currentChar == 'j' && nextChar == 'j' ||
                 currentChar == 'd' && nextChar == 'd' || currentChar == 'b' && nextChar == 'b' ||
                 currentChar == 'p' && nextChar == 'p'))
            {
                return new ReplaceData(currentChar.ToString(), string.Concat(inputChar,'t','u'));
            }

            //�u�����v�u�����v�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'c' && currentChar == 'k' && nextChar == 'k' &&
                (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'o'))
            {
                return new ReplaceData("kk","cc");
            }

            //�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'q' && currentChar == 'k' && nextChar == 'k' && nextChar2 == 'u')
            {
                return new ReplaceData("kk", "qq");
            }

            //�u�����v�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'c' && currentChar == 's' && nextChar == 's' &&
                (nextChar2 == 'i' || nextChar2 == 'e'))
            {
                return new ReplaceData("ss", "cc");
            }

            //�u������v�u������v�u�������v�u������v�̏_��ȓ���
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'y')
            {
                return new ReplaceData("tt", "cc");
            }

            //�u�����v�̏_��ȓ���
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'i')
            {
                return new ReplaceData("tt", "cch");
            }

            //�ul�v�Ɓux�v�̊��S�݊���
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