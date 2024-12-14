using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.TypingUtil;

namespace gaw241201.View
{
    public class TypingTextView : MonoBehaviour, ITextRegisterableView, ICorrectInputEnterableView, IRestrictionRegisterableView, ISelectDataRegisterableView
    {
        const string c_typedName = "Typed";
        const string c_untypedName = "Untyped";
        const string c_startStylePrefix = "<style=";
        const string c_startStyleSuffix = ">";
        const string c_closeStyle = "</style>";

        [Inject] IGazable _gazable;
        [SerializeField] private TextMeshProUGUI _tmpSample; 
        [SerializeField] private TextMeshProUGUI _tmpQuestion;

        string _textCache = "";
        List<SelectionData> _selectionDataCache = new List<SelectionData>();
        List<char> _restrictionList = new List<char>();

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


        public void ResetText()
        {
            _tmpSample.text = "";
            _tmpQuestion.text = "";
        }

        public void SetSampleText(string s)
        {
            _tmpSample.text = s;
        }

        public void RegisterText(string text)
        {
            Log.DebugAssert(_textCache == "");
            _textCache = text;
        }

        public void RegisterSelectData(List<SelectionData> selectDataList)
        {
            Log.DebugAssert(_selectionDataCache.Count == 0);
            _selectionDataCache = selectDataList;
        }

        public void RegisterRestriction(List<char> restrictionList)
        {
            _restrictionList = restrictionList;
        }

        public void EnterCorrectInput(int index)
        {
            _tmpQuestion.text = GetTextTypedTagged(_textCache,index);
            _textCache = "";

            _gazable.Gaze((Vector2)Camera.main.WorldToScreenPoint(_tmpQuestion.transform.position) +
    Vector2.right * _tmpQuestion.preferredWidth * (-0.5f + ((float)index) / _tmpQuestion.GetParsedText().Length));

            SetSelectData(_tmpQuestion, _selectionDataCache, index);

            SetRestriction(_tmpQuestion, _itemViewList, _restrictionList, index);
        }

        const float c_yOffset = 50f;
        const float c_yInterval = 25f;
        [SerializeField] SelectDataItemView _selectDataItemViewPrefab;
        
        List<SelectDataItemView> _itemViewList = new List<SelectDataItemView>();

        void SetSelectData(TextMeshProUGUI parentText, List<SelectionData> selectionDataList, int charIndex)
        {
            for (int i = 0; i < _itemViewList.Count; i++)
            {
                Destroy(_itemViewList[i].gameObject);
            }
            _itemViewList = new List<SelectDataItemView> ();


            for(int i = 0; i < selectionDataList.Count; i++) 
            {
                var itemView = Instantiate<SelectDataItemView>(_selectDataItemViewPrefab, parentText.transform);
                itemView.SetText(GetTagString(c_untypedName) + selectionDataList[i].StringReplaceTo + c_closeStyle);
                itemView.transform.localPosition = _tmpQuestion.GetCharacterLocalPosition(charIndex) + Vector3.down * c_yInterval * i;
                _itemViewList.Add(itemView);
            }
        }

        const string c_restrictedName = "Restricted";

        void SetRestriction(TextMeshProUGUI parentText, List<SelectDataItemView> selectionDataList, List<char> restrictionList, int charIndex)
        {
            for (int i = 0; i < restrictionList.Count; i++)
            {
                List<string> splitted = SplitAtUntypedTag(parentText.text);

                string typedReplaceTo = GetTagString(c_typedName + c_restrictedName) + restrictionList[i] + c_closeStyle;
                string untypedReplaceTo = GetTagString(c_untypedName + c_restrictedName) + restrictionList[i] + c_closeStyle;
                splitted[0]  = ReplacedTextOutTag(splitted[0], restrictionList[i].ToString(), typedReplaceTo);
                splitted[1] = ReplacedTextOutTag(splitted[1], restrictionList[i].ToString(), untypedReplaceTo);
                parentText.text = ConcatSplittedTypingText(splitted);

                foreach(var item in selectionDataList)
                {
                    item.SetText(ReplacedTextOutTag(item.GetText(), restrictionList[i].ToString(), untypedReplaceTo));
                }
            }
        }

        string GetTextTypedTagged(string _viewString, int _viewIndex)

        {  //–â‘è•¶Žš—ñ‚ðUntyped‚Ætyped‚É•ª‚¯‚é
            string beforeReplaceText = GetTagString(c_typedName);
            for (int i = 0; i < _viewString.Length; i++)
            {
                if (_viewString[i] == '@')
                {
                    break;
                }

                if (i == _viewIndex)
                {
                    beforeReplaceText += c_closeStyle + GetTagString(c_untypedName);
                }

                beforeReplaceText += _viewString[i];
            }

            beforeReplaceText += c_closeStyle;



            //—^‚¦‚ç‚ê‚½•¶Žš—ñ‚ð•Ê‚Ì•¶Žš—ñ‚É’u‚«Š·‚¦‚éˆ—
            List<string> splitted = SplitAtUntypedTag(beforeReplaceText);

            foreach (KeyValuePair<char, string> keyValuePair in replacingDictionary)
            {
                List<string> replacedString = new List<string>();

                for (int j = 0; j < splitted.Count; j++)
                {
                    string s = "<sprite name=\"" + keyValuePair.Value + "\">";
                    s += style[j].styleOpeningDefinition;
                    s = s.Replace("><", " ");

                    replacedString.Add(s);
                    splitted[j] = ReplacedTextOutTag(splitted[j], keyValuePair.Key.ToString(), replacedString[j]); 
                }
            }

            return ConcatSplittedTypingText(splitted);
        }

        string ReplacedTextOutTag(string sentence, string replaced, string replaceTo)
        {
            return Regex.Replace(sentence, $@"(?<!<[^>]*?){Regex.Escape(replaced)}(?![^<]*?>)", replaceTo);
        }

        string GetTagString(string tag)
        {
            return c_startStylePrefix + tag + c_startStyleSuffix;
        }

        List<string> SplitAtUntypedTag(string text)
        {
            return text.Split(GetTagString(c_untypedName)).ToList();
        }

        string ConcatSplittedTypingText(List<string> splitted)
        {

            string result = splitted[0];

            for (int i = 1; i < splitted.Count; i++)
            {
                result += GetTagString(c_untypedName);
                result += splitted[i];

            }

            return result;
        }
    }
}