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

        static readonly Dictionary<char, string> replacingDictionary = new Dictionary<char, string>()
        {
            {' ',"space" }
        };

        TMP_StyleSheet styleSheet;
        List<TMP_Style> style;
        public void Initialize()
        {
            styleSheet = TMP_Settings.defaultStyleSheet;
            style = new List<TMP_Style>();
            style.Add(styleSheet.GetStyle(c_typedName));
            style.Add(styleSheet.GetStyle(c_untypedName));
        }

        public string GenerateQuestionText(List<char> questionCharList, int charIndex)
        {
            //–â‘è•¶Žš—ñ‚ðUntyped‚Ætyped‚É•ª‚¯‚é
            string beforeReplaceText = c_typedStyle;
            for (int i = 0; i < questionCharList.Count; i++)
            {
                if (questionCharList[i] == '@')
                {
                    break;
                }

                if (i == charIndex)
                {
                    beforeReplaceText += c_closeStyle + c_untypedStyle;
                }

                beforeReplaceText += questionCharList[i];
            }

            beforeReplaceText += c_closeStyle;





            //—^‚¦‚ç‚ê‚½•¶Žš—ñ‚ð•Ê‚Ì•¶Žš—ñ‚É’u‚«Š·‚¦‚éˆ—
            List<string> splitted = beforeReplaceText.Split(c_untypedStyle).ToList();

            foreach (KeyValuePair<char,string> keyValuePair in replacingDictionary)
            {
                List<string> replacedString = new List<string>();

                for (int j = 0; j < splitted.Count; j++)
                {
                    string s = "<sprite name=\"" + keyValuePair.Value + "\">";
                    s += style[j].styleOpeningDefinition;
                    s = s.Replace("><", " ");

                    replacedString.Add(s);
                    splitted[j] = Regex.Replace(splitted[j], $@"(?<!<[^>]*?){Regex.Escape(keyValuePair.Key.ToString())}(?![^<]*?>)", replacedString[j]);
                }
            }

            string result = splitted[0];

            for (int i = 1; i < splitted.Count; i++)
            {
                result += c_untypedStyle;
                result += splitted[i];

            }


            return result;
        }
    }
}