using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201 { 
    public static class TypingUtil
    {
        public const char c_tagStart = '<';
        public const char c_tagEnd = '>';

        public static string ConvertToString(List<char> charList)
        {
            return string.Concat(charList.ToArray());
        }

        public static string ReadTag(int index, string sentence)
        {
            int endIndex = sentence.IndexOf(c_tagEnd, index);
            return sentence.Substring(index + 1, endIndex - (index + 1));
        }

        public static int CountCharactersInBrackets(string sentence, int index)
        {
            if (index >= sentence.Length)
            {
                index = sentence.Length - 1; // �͈͊O��h��
            }

            int count = 0;
            bool insideBrackets = false;

            for (int i = 0; i <= index; i++)
            {
                if (sentence[i] == '<')
                {
                    if (!insideBrackets) // �V���� '<' �����o
                    {
                        insideBrackets = true;
                        count++; // '<' ���J�E���g
                    }
                    else
                    {
                        // �����ȓ�d '<'�A�J�E���g�𖳌���
                        insideBrackets = false;
                        count = 0;
                    }
                }
                else if (sentence[i] == '>')
                {
                    if (insideBrackets) // �Ή����� '>' ������
                    {
                        count++; // '>' ���J�E���g
                        insideBrackets = false;
                    }
                }
                else if (insideBrackets)
                {
                    count++;
                }
            }

            // ���[�v�I����A������ '<' ������ꍇ�̓J�E���g�𖳌���
            if (insideBrackets)
            {
                count = 0;
            }

            return count;
        }
        public static string RemoveBracketsAndContents(string sentence)
        {
            bool insideBrackets = false;
            var result = new System.Text.StringBuilder();
            int openBracketCount = 0;

            foreach (char c in sentence)
            {
                if (c == '<')
                {
                    if (!insideBrackets)
                    {
                        // ���߂� '<' ���������ꍇ
                        openBracketCount++;
                    }
                    insideBrackets = true;
                }
                else if (c == '>')
                {
                    if (insideBrackets)
                    {
                        // '>' �����������ꍇ
                        openBracketCount--;
                        if (openBracketCount == 0)
                        {
                            insideBrackets = false;
                        }
                    }
                }
                else if (!insideBrackets)
                {
                    result.Append(c);
                }
            }

            // '<' �݂̂ŏI���ꍇ���l�����Č��ʂ�Ԃ�
            if (openBracketCount > 0)
            {
                return sentence; // ���͂̂܂ܕԂ�
            }

            return result.ToString();
        }
        public static string ReplaceFirstOccurrence(string source, string oldValue, string newValue, int startIndex)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(oldValue))
            {
                return source;
            }

            int index = source.IndexOf(oldValue, startIndex);
            if (index < 0)
            {
                Log.DebugAssert("�u���悪������܂���B source : " + source + "oldValue : " + oldValue + "startIndex : " + startIndex);
                return source; // "oldValue" ��������Ȃ��ꍇ�A���̕���������̂܂ܕԂ�
            }

            // �ŏ��Ɍ�������������u��������
            Log.DebugLog("�u��");
            return source.Substring(0, index) + newValue + source.Substring(index + oldValue.Length);
        }

    }
    public static class TMP_TextExtensionMethods
    {
        public static Vector3 GetCharacterLocalPosition
        (
            this TMP_Text self,
            int index
        )
        {
            var characterInfo = self.textInfo.characterInfo[index];

            return !characterInfo.isVisible
                    ? Vector3.zero
                    : self.transform.localPosition + (characterInfo.topLeft + characterInfo.bottomLeft) * 0.5f
                ;
        }
    }
}