using Cysharp.Threading.Tasks;
using NUnit.Framework;
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
    public class TypingTextView : MonoBehaviour, ITextRegisterableView, ICorrectInputEnterableView, IRestrictionRegisterableView, ISelectDataRegisterableView, ISelectionDataWithIndexCatchableFake
    {
        const string c_typedName = "Typed";
        const string c_untypedName = "Untyped";
        const string c_startStylePrefix = "<style=";
        const string c_startStyleSuffix = ">";
        const string c_closeStyle = "</style>";

        [Inject] ITypeMessagePublisher _messagePublisher;
        [Inject] IGazable _gazable;
        [SerializeField] private TextMeshProUGUI _tmpSample; 
        [SerializeField] private TextMeshProUGUI _tmpQuestion;
        [SerializeField] TextMover _textMover;

        string _textCache = "";
        List<ReplaceData> _selectionDataCache = null;
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

        public void EnterCorrectInput(int index)
        {
            _tmpQuestion.text = GetTextTaggedTyped(_textCache, index);
            _textCache = "";

            _messagePublisher.OnType((Vector2)Camera.main.WorldToScreenPoint(_tmpQuestion.transform.position) +
    Vector2.right * _tmpQuestion.preferredWidth * (-0.5f + ((float)index) / _tmpQuestion.GetParsedText().Length));

            if (index > 0)
            {
                _textMover.HighlightText(index - 1);
                if (index > 1)
                {
                    _textMover.LowlightText(index - 2);
                }
            }

          //  SetSelectData(_tmpQuestion, _selectionDataCache, index);

            SetRestriction(_tmpQuestion, _itemViewList, _restrictionList, index);


            //fake
            if (_fakeIndexList.Count > 0)
            {
                Log.DebugLog(_fakeIndexList[0].Index);
            }
            Log.DebugLog(index);
            if (_fakeIndexList.Exists(x => x.Index < index)){
                OnReplacedListSelected(null);
                _fakeIndexList = new List<SelectionDataWithIndex>();
            }
            
        }

        public void ResetText()
        {
            _tmpSample.text = "";
            _tmpQuestion.text = "";
        }

        public void SetSampleText(string s)
        {
            //fake 初期化処理をちゃんと実装したい SampleTextセットするとことかに
            Show();
            _tmpSample.text = s;
        }

        public void RegisterText(string text)
        {
            Log.DebugAssert(_textCache == "");
            _textCache = text;
        }

        public void RegisterSelectData(List<ReplaceData> selectDataList)
        {
            Log.Comment("selectData登録");
            Log.DebugAssert(_selectionDataCache == null);
            _selectionDataCache = selectDataList;
        }

        public void RegisterRestriction(List<char> restrictionList)
        {
            _restrictionList = restrictionList;
        }


        const float c_yOffset = 50f;
        const float c_yInterval = 25f;
        [SerializeField] SelectDataItemView _selectDataItemViewPrefab;
        
        List<SelectDataItemView> _itemViewList = new List<SelectDataItemView>();
        List<SelectionDataWithIndex> _fakeIndexList = new List<SelectionDataWithIndex>();

        public void SetSelectionDataWithIndex(List<SelectionDataWithIndex> list) 
        {
            _fakeIndexList = new List<SelectionDataWithIndex>();

            foreach(var item in list)
            {
                _fakeIndexList.Add(item);
                List<ReplaceData> fakeList = new List<ReplaceData>();
                fakeList.Add(item.ReplaceData);
                SetSelectData(_tmpQuestion, fakeList, item.Index);
            }
        }

        public void OnReplacedListSelected(List<ReplaceData> list)
        {
            //暫定処理
            for (int i = 0; i < _itemViewList.Count; i++)
            {
                Destroy(_itemViewList[i].gameObject);
            }
            _itemViewList = new List<SelectDataItemView>();
        }

        void SetSelectData(TextMeshProUGUI parentText, List<ReplaceData> selectionDataList, int charIndex)
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
                itemView.SetPosition(_tmpQuestion,charIndex,Vector3.down * c_yInterval * 2);
                _itemViewList.Add(itemView);
            }

            _selectionDataCache = null;
        }

        const string c_restrictedName = "Restricted";

        void SetRestriction(TextMeshProUGUI textView, List<SelectDataItemView> selectionDataList, List<char> restrictionList, int charIndex)
        {
            for (int i = 0; i < restrictionList.Count; i++)
            {
                List<string> splitted = SplitAtUntypedTag(textView.text);

                string typedReplaceTo = GetTagString(c_typedName + c_restrictedName) + restrictionList[i] + c_closeStyle;
                string untypedReplaceTo = GetTagString(c_untypedName + c_restrictedName) + restrictionList[i] + c_closeStyle;
                splitted[0]  = ReplacedTextOutTag(splitted[0], restrictionList[i].ToString(), typedReplaceTo);
                splitted[1] = ReplacedTextOutTag(splitted[1], restrictionList[i].ToString(), untypedReplaceTo);
                textView.text = ConcatSplittedTypingText(splitted);

                foreach(var item in selectionDataList)
                {
                    item.SetText(ReplacedTextOutTag(item.GetText(), restrictionList[i].ToString(), untypedReplaceTo));
                }
            }
        }

        string GetTextTaggedTyped(string _viewString, int _viewIndex)

        {  //問題文字列をUntypedとtypedに分ける
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

            //与えられた文字列を別の文字列に置き換える処理
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
            List<string> returnable = text.Split(GetTagString(c_untypedName)).ToList();
            
            if(returnable.Count == 1)
            {
                returnable.Insert(0, "");
            }
            Log.DebugAssert(returnable.Count == 2);
            return returnable;
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


        GameObject _root;

        void Start()
        {
            _root = transform.Find("Root").gameObject;
            UnShow();
        }

        void Show()
        {
            _root.SetActive(true);
        }

        void UnShow()
        {
            _root.SetActive(false);

        }
    }
}