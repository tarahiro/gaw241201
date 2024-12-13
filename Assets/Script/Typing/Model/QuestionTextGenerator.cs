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
using static gaw241201.View.TypingUtil;

namespace gaw241201.View
{
    public class QuestionTextGenerator : IQuestionTextGenerator
    {
        const string c_typedName = "typed";
        const string c_untypedName = "untyped";
        const string c_startStylePrefix = "<style=";
        const string c_startStyleSuffix = ">";
        const string c_closeStyle = "</style>";
        const string c_typedStyle = c_startStylePrefix + c_typedName + c_startStyleSuffix;
        const string c_untypedStyle = c_startStylePrefix + c_untypedName + c_startStyleSuffix;

        bool _isInitialized = false;

        Subject<string> _correctInputted = new Subject<string>();
        public IObservable<string> CorrectInputted => _correctInputted;


        static readonly Dictionary<char, string> replacingDictionary = new Dictionary<char, string>()
        {
            {' ',"space" }
        };

        TMP_StyleSheet styleSheet;
        List<TMP_Style> style;
        void Initialize()
        {
            styleSheet = TMP_Settings.defaultStyleSheet;
            style = new List<TMP_Style>();
            style.Add(styleSheet.GetStyle(c_typedName));
            style.Add(styleSheet.GetStyle(c_untypedName));
            _isInitialized = true;
        }

        public void GenerateQuestionText(string questionChar, int charIndex)
        {
            if (!_isInitialized)
            {
                Initialize();
            }


            int _viewIndex = charIndex - CountCharactersBetweenBrackets(questionChar, charIndex);
            string _viewString = RemoveBracketsAndContents(questionChar);


            //問題文字列をUntypedとtypedに分ける
            string beforeReplaceText = c_typedStyle;
            for (int i = 0; i < _viewString.Length; i++)
            {
                if (_viewString[i] == '@')
                {
                    break;
                }

                if (i == _viewIndex)
                {
                    beforeReplaceText += c_closeStyle + c_untypedStyle;
                }

                beforeReplaceText += _viewString[i];
            }

            beforeReplaceText += c_closeStyle;



            //与えられた文字列を別の文字列に置き換える処理
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

            //通知
            Log.Comment("問題文の更新完了");
            _correctInputted.OnNext(result);
        }
    }
}