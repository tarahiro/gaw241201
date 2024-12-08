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

namespace gaw241201.View
{
    public class TypingItemView : MonoBehaviour
    {
        IKeyInputJudger _keyInputJudger;
        IKeyCodeToCharConverter _keyToCharConverter;
        IQuestionTextGenerator _questionTextGenerator;
        TypingViewArgs _viewArgsList;
        IGazable _gazable;

        [SerializeField] private TextMeshProUGUI _tmpSample; // ここに日本語表示のTextMeshProをアタッチする。
        [SerializeField] private TextMeshProUGUI _tmpQuestion; // ここにローマ字表示のTextMeshProをアタッチする。

        private readonly List<char> _questionCharList = new List<char>();

        private int _charIndex;
        bool _isWindows = true;
        bool _isMac = false;
        bool _isEndLoop = false;

        public void Construct(TypingViewArgs args, IGazable gazable, IKeyInputJudger keyInputJudger, IKeyCodeToCharConverter keyToCharConverter, IQuestionTextGenerator questionTextGenerator)
        {
            _viewArgsList = args;
            _gazable = gazable;
            _keyInputJudger = keyInputJudger;
            _keyToCharConverter = keyToCharConverter;
            _questionTextGenerator = questionTextGenerator;
        }

        public async UniTask Enter(CancellationToken cancellationToken)
        {
            //初期設定
            InitializeQuestion();

            //すべての文字が終わるまで待って、処理を返す
            while (!_isEndLoop & !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Yield(PlayerLoopTiming.Update);
                CheckInput();
            }
        }

        void InitializeQuestion()
        {
            //初期化
            _questionCharList.Clear();
            _charIndex = 0;

            // 問題の初期状態を設定
            char[] characters = _viewArgsList.QuestionText.ToCharArray();
            foreach (char character in characters)
            {
                _questionCharList.Add(character);
            }
            _questionCharList.Add('@');

            //表示テキストを反映
            _tmpSample.text = _viewArgsList.SampleText;
            
            //問題テキストを反映
            UpdateQuestionText();
        }

        void UpdateQuestionText()
        {
            _tmpQuestion.text = _questionTextGenerator.GenerateQuestionText(_questionCharList, _charIndex);
            _gazable.Gaze((Vector2)Camera.main.WorldToScreenPoint(_tmpQuestion.transform.position) +
                Vector2.right * _tmpQuestion.preferredWidth * (-0.5f + ((float)_charIndex) / _questionCharList.Count));

        }


        void CheckInput()
        {
            if (KeyInputUtil.TryGetKeyDown(out var key))
            {
                if (IsMainInputAccept())
                {
                    _keyToCharConverter.TryConvertKeyCodeToChar(key,out char c);

                    if (_keyInputJudger.IsKeyInputCorrect(c, _charIndex, _questionCharList))
                    {
                        _charIndex++;
                        SoundManager.PlaySE("Key");
                        if (_questionCharList[_charIndex] == '@') // 「@」がタイピングの終わりの判定となる。
                        {
                            _isEndLoop = true;
                        }
                        else
                        {
                            UpdateQuestionText();
                        }
                    }
                }
            }
        }

        bool IsMainInputAccept()
        {

            return true;
        }
    }
}