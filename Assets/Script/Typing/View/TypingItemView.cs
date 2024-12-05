using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static PlasticGui.WorkspaceWindow.CodeReview.ReviewChanges.Summary.CommentSummaryData;

namespace gaw241201.View
{
    public class TypingItemView : MonoBehaviour
    {

        TypingViewArgs _viewArgsList;
        IGazable _gazable;


        [SerializeField] private TextMeshProUGUI textJapanese; // �����ɓ��{��\����TextMeshPro���A�^�b�`����B
        [SerializeField] private TextMeshProUGUI textRoman; // �����Ƀ��[�}���\����TextMeshPro���A�^�b�`����B

        private readonly List<char> _roman = new List<char>();

        private int _romanIndex;
        bool _isWindows = true;
        bool _isMac = false;
        bool _isEndLoop = false;

        public void Construct(TypingViewArgs args, IGazable gazable)
        {
            _viewArgsList = args;
            _gazable = gazable;
        }

        public async UniTask Enter(CancellationToken cancellationToken)
        {
            InitializeQuestion();

            while (!_isEndLoop & !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                CheckInput();
            }
        }

        void CheckInput()
        {
            if (KeyInputUtil.TryGetKeyDown(out var key))
            {
                if (IsMainInputAccept())
                {
                    switch (InputKey(GetCharFromKeyCode(key)))
                    {
                        case 1: // �����^�C�v��
                            _romanIndex++;
                            SoundManager.PlaySE("Key");
                            if (_roman[_romanIndex] == '@') // �u@�v���^�C�s���O�̏I���̔���ƂȂ�B
                            {
                                _isEndLoop = true;
                            }
                            else
                            {
                                SetRomanText();
                            }
                            break;
                        case 2: // �~�X�^�C�v��
                            break;
                    }
                }
            }
        }
        bool IsMainInputAccept()
        {

            return true;
        }


        void InitializeQuestion()
        {

            _roman.Clear();

            _romanIndex = 0;

            string s = "";

            char[] characters = _viewArgsList.RomanText.ToCharArray();

            foreach (char character in characters)
            {
                _roman.Add(character);
            }

            _roman.Add('@');

            textJapanese.text = _viewArgsList.JpText;
            SetRomanText();

        }

        void SetRomanText()
        {
            textRoman.text = GenerateTextRoman();
            _gazable.Gaze((Vector2)Camera.main.WorldToScreenPoint(textRoman.transform.position) +
                Vector2.right * textRoman.preferredWidth * (-0.5f + ((float)_romanIndex) / _roman.Count));

        }

        string GenerateTextRoman()
        {
            string text = "<style=typed>";
            for (int i = 0; i < _roman.Count; i++)
            {
                if (_roman[i] == '@')
                {
                    break;
                }

                if (i == _romanIndex)
                {
                    text += "</style><style=untyped>";
                }

                text += _roman[i];
            }

            text += "</style>";

            return text;
        }

        int InputKey(char inputChar)
        {
            char prevChar3 = _romanIndex >= 3 ? _roman[_romanIndex - 3] : '\0';
            char prevChar2 = _romanIndex >= 2 ? _roman[_romanIndex - 2] : '\0';
            char prevChar = _romanIndex >= 1 ? _roman[_romanIndex - 1] : '\0';
            char currentChar = _roman[_romanIndex];
            char nextChar = _roman[_romanIndex + 1];
            char nextChar2 = nextChar == '@' ? '@' : _roman[_romanIndex + 2];

            if (inputChar == '\0')
            {
                return 0;
            }

            if (inputChar == currentChar)
            {
                return 1;
            }

            //�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'y' && currentChar == 'i' &&
                (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' || prevChar == 'u' || prevChar == 'e' ||
                 prevChar == 'o'))
            {
                _roman.Insert(_romanIndex, 'y');
                return 1;
            }

            if (_isWindows && inputChar == 'y' && currentChar == 'i' && prevChar == 'n' && prevChar2 == 'n' &&
                prevChar3 != 'n')
            {
                _roman.Insert(_romanIndex, 'y');
                return 1;
            }

            if (_isWindows && inputChar == 'y' && currentChar == 'i' && prevChar == 'n' && prevChar2 == 'x')
            {
                _roman.Insert(_romanIndex, 'y');
                return 1;
            }

            //�u���v�̏_��ȓ��́i�uwhu�v��Windows�̂݁j
            if (inputChar == 'w' && currentChar == 'u' && (prevChar == '\0' || prevChar == 'a' || prevChar == 'i' ||
                                                           prevChar == 'u' || prevChar == 'e' || prevChar == 'o'))
            {
                _roman.Insert(_romanIndex, 'w');
                return 1;
            }

            if (inputChar == 'w' && currentChar == 'u' && prevChar == 'n' && prevChar2 == 'n' && prevChar3 != 'n')
            {
                _roman.Insert(_romanIndex, 'w');
                return 1;
            }

            if (inputChar == 'w' && currentChar == 'u' && prevChar == 'n' && prevChar2 == 'x')
            {
                _roman.Insert(_romanIndex, 'w');
                return 1;
            }

            if (_isWindows && inputChar == 'h' && prevChar2 != 't' && prevChar2 != 'd' && prevChar == 'w' &&
                currentChar == 'u')
            {
                _roman.Insert(_romanIndex, 'h');
                return 1;
            }

            //�u���v�u���v�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'c' && prevChar != 'k' &&
                currentChar == 'k' && (nextChar == 'a' || nextChar == 'u' || nextChar == 'o'))
            {
                _roman[_romanIndex] = 'c';
                return 1;
            }

            //�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'q' && prevChar != 'k' && currentChar == 'k' && nextChar == 'u')
            {
                _roman[_romanIndex] = 'q';
                return 1;
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'i')
            {
                _roman.Insert(_romanIndex, 'h');
                return 1;
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'j' && currentChar == 'z' && nextChar == 'i')
            {
                _roman[_romanIndex] = 'j';
                return 1;
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
            if (inputChar == 'h' && prevChar == 's' && currentChar == 'y')
            {
                _roman[_romanIndex] = 'h';
                return 1;
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
            if (inputChar == 'z' && prevChar != 'j' && currentChar == 'j' &&
                (nextChar == 'a' || nextChar == 'u' || nextChar == 'e' || nextChar == 'o'))
            {
                _roman[_romanIndex] = 'z';
                _roman.Insert(_romanIndex + 1, 'y');
                return 1;
            }

            if (inputChar == 'y' && prevChar == 'j' &&
                (currentChar == 'a' || currentChar == 'u' || currentChar == 'e' || currentChar == 'o'))
            {
                _roman.Insert(_romanIndex, 'y');
                return 1;
            }

            //�u���v�u���v�̏_��ȓ��́iWindows�̂݁j
            if (_isWindows && inputChar == 'c' && prevChar != 's' && currentChar == 's' &&
                (nextChar == 'i' || nextChar == 'e'))
            {
                _roman[_romanIndex] = 'c';
                return 1;
            }

            //�u���v�̏_��ȓ���
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'i')
            {
                _roman[_romanIndex] = 'c';
                _roman.Insert(_romanIndex + 1, 'h');
                return 1;
            }

            //�u����v�u����v�u�����v�u����v�̏_��ȓ���
            if (inputChar == 'c' && prevChar != 't' && currentChar == 't' && nextChar == 'y')
            {
                _roman[_romanIndex] = 'c';
                return 1;
            }

            //�ucya�v=>�ucha�v
            if (inputChar == 'h' && prevChar == 'c' && currentChar == 'y')
            {
                _roman[_romanIndex] = 'h';
                return 1;
            }

            //�u�v�̏_��ȓ���
            if (inputChar == 's' && prevChar == 't' && currentChar == 'u')
            {
                _roman.Insert(_romanIndex, 's');
                return 1;
            }

            //�u���v�u���v�u���v�u���v�̏_��ȓ���
            if (inputChar == 'u' && prevChar == 't' && currentChar == 's' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                _roman[_romanIndex] = 'u';
                _roman.Insert(_romanIndex + 1, 'x');
                return 1;
            }

            if (inputChar == 'u' && prevChar2 == 't' && prevChar == 's' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                _roman.Insert(_romanIndex, 'u');
                _roman.Insert(_romanIndex + 1, 'x');
                return 1;
            }

            //�u�Ă��v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 't' && currentChar == 'h' && nextChar == 'i')
            {
                _roman[_romanIndex] = 'e';
                _roman.Insert(_romanIndex + 1, 'x');
                return 1;
            }

            //�u�ł��v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'i')
            {
                _roman[_romanIndex] = 'e';
                _roman.Insert(_romanIndex + 1, 'x');
                return 1;
            }

            //�u�ł�v�̏_��ȓ���
            if (inputChar == 'e' && prevChar == 'd' && currentChar == 'h' && nextChar == 'u')
            {
                _roman[_romanIndex] = 'e';
                _roman.Insert(_romanIndex + 1, 'x');
                _roman.Insert(_romanIndex + 2, 'y');
                return 1;
            }

            //�u�Ƃ��v�̏_��ȓ���
            if (inputChar == 'o' && prevChar == 't' && currentChar == 'w' && nextChar == 'u')
            {
                _roman[_romanIndex] = 'o';
                _roman.Insert(_romanIndex + 1, 'x');
                return 1;
            }
            //�u�ǂ��v�̏_��ȓ���
            if (inputChar == 'o' && prevChar == 'd' && currentChar == 'w' && nextChar == 'u')
            {
                _roman[_romanIndex] = 'o';
                _roman.Insert(_romanIndex + 1, 'x');
                return 1;
            }

            //�u�Ӂv�̏_��ȓ���
            if (inputChar == 'f' && currentChar == 'h' && nextChar == 'u')
            {
                _roman[_romanIndex] = 'f';
                return 1;
            }

            //�u�ӂ��v�u�ӂ��v�u�ӂ��v�u�ӂ��v�̏_��ȓ��́i�ꕔMac�̂݁j
            if (inputChar == 'w' && prevChar == 'f' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                _roman.Insert(_romanIndex, 'w');
                return 1;
            }

            if (inputChar == 'y' && prevChar == 'f' && (currentChar == 'i' || currentChar == 'e'))
            {
                _roman.Insert(_romanIndex, 'y');
                return 1;
            }

            if (inputChar == 'h' && prevChar != 'f' && currentChar == 'f' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                if (_isMac)
                {
                    _roman[_romanIndex] = 'h';
                    _roman.Insert(_romanIndex + 1, 'w');
                }
                else
                {
                    _roman[_romanIndex] = 'h';
                    _roman.Insert(_romanIndex + 1, 'u');
                    _roman.Insert(_romanIndex + 2, 'x');
                }
                return 1;
            }

            if (inputChar == 'u' && prevChar == 'f' &&
                (currentChar == 'a' || currentChar == 'i' || currentChar == 'e' || currentChar == 'o'))
            {
                _roman.Insert(_romanIndex, 'u');
                _roman.Insert(_romanIndex + 1, 'x');
                return 1;
            }

            if (_isMac && inputChar == 'u' && prevChar == 'h' && currentChar == 'w' &&
                (nextChar == 'a' || nextChar == 'i' || nextChar == 'e' || nextChar == 'o'))
            {
                _roman[_romanIndex] = 'u';
                _roman.Insert(_romanIndex + 1, 'x');
                return 1;
            }

            //�u��v�̏_��ȓ��́i�un'�v�ɂ͖��Ή��j
            if (inputChar == 'n' && prevChar2 != 'n' && prevChar == 'n' && currentChar != 'a' && currentChar != 'i' &&
                currentChar != 'u' && currentChar != 'e' && currentChar != 'o' && currentChar != 'y')
            {
                _roman.Insert(_romanIndex, 'n');
                return 1;
            }

            if (inputChar == 'x' && prevChar != 'n' && currentChar == 'n' && nextChar != 'a' && nextChar != 'i' &&
                nextChar != 'u' && nextChar != 'e' && nextChar != 'o' && nextChar != 'y')
            {
                if (nextChar == 'n')
                {
                    _roman[_romanIndex] = 'x';
                }
                else
                {
                    _roman.Insert(_romanIndex, 'x');
                }
                return 1;
            }

            //�u�����v�u�����v�u�����v�𕪉�����
            if (inputChar == 'u' && currentChar == 'w' && nextChar == 'h' && (nextChar2 == 'a' || nextChar2 == 'i' || nextChar2 == 'e' || nextChar2 == 'o'))
            {
                _roman[_romanIndex] = 'u';
                _roman[_romanIndex] = 'x';
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
                    _roman[_romanIndex] = 'i';
                    _roman.Insert(_romanIndex + 1, 'x');
                }
                else
                {
                    _roman.Insert(_romanIndex, 'i');
                    _roman.Insert(_romanIndex + 1, 'x');
                }
                return 1;
            }

            //�u����v�u����v�Ȃǂ𕪉�����
            if (inputChar == 'i' &&
                (currentChar == 'a' || currentChar == 'u' || currentChar == 'e' || currentChar == 'o') &&
                (prevChar2 == 's' || prevChar2 == 'c') && prevChar == 'h')
            {
                if (nextChar == 'e')
                {
                    _roman.Insert(_romanIndex, 'i');
                    _roman.Insert(_romanIndex + 1, 'x');
                }
                else
                {
                    _roman.Insert(_romanIndex, 'i');
                    _roman.Insert(_romanIndex + 1, 'x');
                    _roman.Insert(_romanIndex + 2, 'y');
                }
                return 1;
            }

            //�u����v���uc�v�ŕ�������iWindows����j
            if (_isWindows && inputChar == 'c' && currentChar == 's' && prevChar != 's' && nextChar == 'y' &&
                (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'e' || nextChar2 == 'o'))
            {
                if (nextChar2 == 'e')
                {
                    _roman[_romanIndex] = 'c';
                    _roman[_romanIndex + 1] = 'i';
                    _roman.Insert(_romanIndex + 1, 'x');
                }
                else
                {
                    _roman[_romanIndex] = 'c';
                    _roman.Insert(_romanIndex + 1, 'i');
                    _roman.Insert(_romanIndex + 2, 'x');
                }
                return 1;
            }

            //�u���v�̏_��ȓ���
            if ((inputChar == 'x' || inputChar == 'l') &&
                (currentChar == 'k' && nextChar == 'k' || currentChar == 's' && nextChar == 's' ||
                 currentChar == 't' && nextChar == 't' || currentChar == 'g' && nextChar == 'g' ||
                 currentChar == 'z' && nextChar == 'z' || currentChar == 'j' && nextChar == 'j' ||
                 currentChar == 'd' && nextChar == 'd' || currentChar == 'b' && nextChar == 'b' ||
                 currentChar == 'p' && nextChar == 'p'))
            {
                _roman[_romanIndex] = inputChar;
                _roman.Insert(_romanIndex + 1, 't');
                _roman.Insert(_romanIndex + 2, 'u');
                return 1;
            }

            //�u�����v�u�����v�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'c' && currentChar == 'k' && nextChar == 'k' &&
                (nextChar2 == 'a' || nextChar2 == 'u' || nextChar2 == 'o'))
            {
                _roman[_romanIndex] = 'c';
                _roman[_romanIndex + 1] = 'c';
                return 1;
            }

            //�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'q' && currentChar == 'k' && nextChar == 'k' && nextChar2 == 'u')
            {
                _roman[_romanIndex] = 'q';
                _roman[_romanIndex + 1] = 'q';
                return 1;
            }

            //�u�����v�u�����v�̏_��ȓ��́iWindows����j
            if (_isWindows && inputChar == 'c' && currentChar == 's' && nextChar == 's' &&
                (nextChar2 == 'i' || nextChar2 == 'e'))
            {
                _roman[_romanIndex] = 'c';
                _roman[_romanIndex + 1] = 'c';
                return 1;
            }

            //�u������v�u������v�u�������v�u������v�̏_��ȓ���
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'y')
            {
                _roman[_romanIndex] = 'c';
                _roman[_romanIndex + 1] = 'c';
                return 1;
            }

            //�u�����v�̏_��ȓ���
            if (inputChar == 'c' && currentChar == 't' && nextChar == 't' && nextChar2 == 'i')
            {
                _roman[_romanIndex] = 'c';
                _roman[_romanIndex + 1] = 'c';
                _roman.Insert(_romanIndex + 2, 'h');
                return 1;
            }

            //�ul�v�Ɓux�v�̊��S�݊���
            if (inputChar == 'x' && currentChar == 'l')
            {
                _roman[_romanIndex] = 'x';
                return 1;
            }

            if (inputChar == 'l' && currentChar == 'x')
            {
                _roman[_romanIndex] = 'l';
                return 1;
            }

            return 2;
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
                default: //��L�ȊO�̃L�[�������ꂽ�ꍇ�́unull�����v��Ԃ��B
                    return '\0';
            }
        }
    }
}