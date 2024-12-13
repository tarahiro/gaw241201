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
    public class TypingSentenceController
    {
        [Inject] IKeyInputJudger _keyInputJudger;
        [Inject] RoguelikeCorrectInputHundler _correctInputHundlable;

        string _tagSentence;
        int _tagSentenceIndex;

        Subject<Unit> _ended = new Subject<Unit>();
        public IObservable<Unit> Ended => _ended;

        const char c_tagStart = '<';
        const char c_tagEnd = '>';

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

            List<char> _tempSentence = _tagSentence.ToList();
            if(_keyInputJudger.IsKeyInputCorrect(c,_tagSentenceIndex, _tempSentence))
            {
                //if 有効
                _tagSentence = TypingUtil.ConvertToString(_tempSentence);

                _tagSentenceIndex++;
                if (_tagSentence[_tagSentenceIndex] == c_tagStart)
                {

                    Log.Comment("Todo:タグを読む");
                    Log.DebugLog(ReadTag(_tagSentenceIndex, _tagSentence));

                    Log.DebugLog("Todo;タグの結果を通知");

                    Log.DebugLog("Todo:タグ終了までindexを飛ばす");
                    _tagSentenceIndex = _tagSentence.IndexOf(c_tagEnd, _tagSentenceIndex) + 1;
                }
                Log.DebugLog(_tagSentence[_tagSentenceIndex]);

                int _viewIndex = _tagSentenceIndex -  CountCharactersBetweenBrackets(_tagSentence, _tagSentenceIndex);
                string _viewString = RemoveBracketsAndContents(_tagSentence);
                _correctInputHundlable.OnCorrectInput(_viewString.ToCharArray().ToList(), _viewIndex, out var isEndLoop);

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

            Log.DebugLog(count);
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
    }
}