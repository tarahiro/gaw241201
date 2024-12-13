using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TypingSentenceController : ILeetDataUserView
    {
        [Inject] RoguelikeCorrectInputHundler _correctInputHundlable;
        [Inject] RoguelikeRestrictInputHundler _restrictInputHundlable;
        [Inject] KeyInputProcesser _keyInputProcesser;

        List<SelectionData> selectionDataList = new List<SelectionData>();
        List<LeetCharData> _charDataList;
        List<WordData> _wordDataList = new List<WordData>() { new WordData("alive", "verb"), new WordData("dog", "animal") };
        List<char> _restrictedChar = new List<char>() { 'a','c' };

        string _tagSentence;
        int _tagSentenceIndex;

        Subject<Unit> _ended = new Subject<Unit>();
        public IObservable<Unit> Ended => _ended;

        const char c_tagStart = '<';
        const char c_tagEnd = '>';

        public void Initialize(List<LeetCharData> charDataList)
        {
            _charDataList = charDataList;
        }

        public void Initialize(string tagSentence)
        {
            Log.DebugLog(tagSentence);
            //タグのついた文章を受け取る
            //indexを初期化する
            _tagSentence = tagSentence;
            _tagSentenceIndex = 0;
        }

        public void EnterKey(char c)
        {
            //入力が有効かを判定
            if(_keyInputProcesser.TryKeyProcess(c,_tagSentenceIndex, _tagSentence,selectionDataList, out var selected))
            {
                selectionDataList = new List<SelectionData>();

                if (selected.Count > 0)
                {
                    Log.Comment("検出されたSelectedDataを処理");
                    _tagSentence = ReplaceFirstOccurrence(_tagSentence, selected[0].ReplacedString, selected[0].StringReplaceTo, _tagSentenceIndex);
                }

                _tagSentenceIndex++;

                //tag
                if (_tagSentence[_tagSentenceIndex] == c_tagStart)
                {
                    int _index = _tagSentence.IndexOf(c_tagEnd, _tagSentenceIndex) + 1;

                    if (_tagSentence[_tagSentenceIndex + 1] != '/')
                    {

                        string tag = ReadTag(_tagSentenceIndex, _tagSentence);
                        string substring = _tagSentence.Substring(_index);
                        string word = substring.Substring(0, substring.IndexOf(c_tagStart.ToString() + "/" + tag + c_tagEnd.ToString()));

                        Log.DebugLog(substring);

                        foreach (var wordData in _wordDataList)
                        {
                            if (wordData.Tag == tag)
                            {
                                selectionDataList.Add(new SelectionData(word, wordData.StringReplaceTo));
                            }
                        }
                    }
                    _tagSentenceIndex = _index;
                }

                //leet
                for (int i = 0; i < _charDataList.Count; i++)
                {
                    if (_tagSentence[_tagSentenceIndex] == _charDataList[i].LeetedChar)
                    {
                        for (int j = 0; j < _charDataList[i].ReplaceToStringList.Count; j++)
                        {
                            selectionDataList.Add(new SelectionData(_charDataList[i].LeetedChar.ToString(), _charDataList[i].ReplaceToStringList[j]));
                        }
                    }
                }

                int _viewIndex = _tagSentenceIndex -  CountCharactersBetweenBrackets(_tagSentence, _tagSentenceIndex);
                string _viewString = RemoveBracketsAndContents(_tagSentence);


                bool isEndLoop;
                if (_restrictedChar.Contains(c))
                {
                    _restrictInputHundlable.OnCorrectInput(_viewString.ToCharArray().ToList(), _viewIndex, out isEndLoop);
                }
                else
                {
                    _correctInputHundlable.OnCorrectInput(_viewString.ToCharArray().ToList(), _viewIndex, out isEndLoop);
                }

                if (isEndLoop)
                {
                    _ended.OnNext(Unit.Default);
                }

            }

            //  indexを一つ進める
            //  if タグ開始がある
            //      タグを読む
            //      タグ開始を誰かに通知
            //      タグ開始が終わるまでindexを飛ばす
            //  TextViewに反映
        }

        string ReadTag(int index, string sentence)
        {
            int endIndex = sentence.IndexOf(c_tagEnd, index);
            return sentence.Substring(index + 1, endIndex - (index + 1));
        }
        int CountCharactersBetweenBrackets(string sentence, int index)
        {
            if (index >= sentence.Length)
            {
                index = sentence.Length - 1; // 範囲外を防ぐ
            }

            int count = 0;
            bool insideBrackets = false;

            for (int i = 0; i <= index; i++)
            {
                if (sentence[i] == '<')
                {
                    count++;
                    insideBrackets = true;
                }
                else if (sentence[i] == '>')
                {
                    count++;
                    insideBrackets = false;
                }
                else if (insideBrackets)
                {
                    count++;
                }
            }

            return count;
        }
        string RemoveBracketsAndContents(string sentence)
        {
            bool insideBrackets = false;
            var result = new System.Text.StringBuilder();

            foreach (char c in sentence)
            {
                if (c == '<')
                {
                    insideBrackets = true;
                }
                else if (c == '>')
                {
                    insideBrackets = false;
                }
                else if (!insideBrackets)
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }
        string ReplaceFirstOccurrence(string source, string oldValue, string newValue, int startIndex)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(oldValue))
            {
                return source;
            }

            int index = source.IndexOf(oldValue,startIndex);
            if (index < 0)
            {
                Log.DebugAssert("置換先が見つかりません。 source : " + source + "oldValue : " +oldValue + "startIndex : " + startIndex );
                return source; // "oldValue" が見つからない場合、元の文字列をそのまま返す
            }

            // 最初に見つかった部分を置き換える
            Log.DebugLog("置換");
            return source.Substring(0, index) + newValue + source.Substring(index + oldValue.Length);
        }
    }
}