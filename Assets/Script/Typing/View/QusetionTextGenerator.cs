using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class QusetionTextGenerator : IQuestionTextGenerator
    {
        const string c_typedName = "typed";
        const string c_untypedName = "untyped";
        const string c_startStylePrefix = "<style=";
        const string c_startStyleSuffix = ">";
        const string c_closeStyle = "</style>";
        const string c_typedStyle = c_startStylePrefix + c_typedName + c_startStyleSuffix;
        const string c_untypedStyle = c_startStylePrefix + c_untypedName + c_startStyleSuffix;
        public string GenerateQuestionText(List<char> questionCharList, int charIndex)
        {
            string text = c_typedStyle;
            for (int i = 0; i < questionCharList.Count; i++)
            {
                if (questionCharList[i] == '@')
                {
                    break;
                }

                if (i == charIndex)
                {
                    text += c_closeStyle + c_untypedStyle;
                }

                text += questionCharList[i];
            }

            text += c_closeStyle;

            TMP_StyleSheet styleSheet = TMP_Settings.defaultStyleSheet;

            List<string> splitted = text.Split(c_untypedStyle).ToList();


            //Replaceèàóù

            char replacedChar = ' ';
            string spriteNameReplaceTo = "space";

            List<string> replacedString = new List<string>();
            List<TMP_Style> style = new List<TMP_Style>();
            style.Add(styleSheet.GetStyle(c_typedName));
            style.Add(styleSheet.GetStyle(c_untypedName));



            for (int i = 0; i < splitted.Count; i++)
            {
                string s = "<sprite name=\"" + spriteNameReplaceTo + "\">";
                s += style[i].styleOpeningDefinition;
                s = s.Replace("><", " ");

                replacedString.Add(s);
            }

            for (int i = 0; i < splitted.Count; i++)
            {
                // ê≥ãKï\åªÇ…ÇÊÇÈíuä∑
                splitted[i] = Regex.Replace(splitted[i], @"(?<!<[^>]*?)\s(?![^<]*?>)", replacedString[i]);
            }


            text = splitted[0];

            for (int i = 1; i < splitted.Count; i++)
            {
                text += c_untypedStyle;
                text += splitted[i];

            }


            return text;
        }
    }
}