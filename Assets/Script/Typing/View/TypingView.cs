using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using TMPro;

namespace gaw241201.View
{
    public class TypingView
    {
        [Inject] IKeyInputJudger _keyInputJudger;
        [Inject] IQuestionTextGenerator _questionTextGenerator;
        [Inject] TypingItemView _item;
        TypingViewArgs _viewArgsList;


        private readonly List<char> _questionCharList = new List<char>();

        private int _charIndex;
        bool _isWindows = true;
        bool _isMac = false;
        bool _isEndLoop = false;

        Subject<Unit> _exited = new Subject<Unit>();
        public IObservable<Unit> Exited => _exited;

        public void Initialize()
        {
            _questionTextGenerator.Initialize();
        }

        public async UniTask Enter(TypingViewArgs args)
        {
            _viewArgsList = args;

            //�����ݒ�
            _isEndLoop = false;
            InitializeQuestion();
            var v = args.CancellationToken.Register(OnExit);

            //���ׂĂ̕������I���܂ő҂��āA������Ԃ�
            while (!_isEndLoop)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: args.CancellationToken);
                CheckInput();
            }

            v.Dispose();
            OnExit();
        }

        private void OnExit()
        {
            Log.Comment("TypingItemView�̏I������");
            _item.ResetText();
            _exited.OnNext(Unit.Default);
        }

        void InitializeQuestion()
        {
            Log.Comment("TypingItemView�̏I������");


            //������
            _questionCharList.Clear();
            _charIndex = 0;

            // ���̏�����Ԃ�ݒ�
            char[] characters = _viewArgsList.QuestionText.ToCharArray();
            foreach (char character in characters)
            {
                _questionCharList.Add(character);
            }
            _questionCharList.Add('@');

            //�\���e�L�X�g�𔽉f
            _item.SetSampleText(_viewArgsList.SampleText);

            //���e�L�X�g�𔽉f
            UpdateQuestionText();
        }

        void UpdateQuestionText()
        {
            _item.SetQuestionText(_questionTextGenerator.GenerateQuestionText(_questionCharList, _charIndex),_charIndex,_questionCharList.Count);
        }

        void CheckInput()
        {
            if (IsMainInputAccept())
            {

                for (int i = 0; i < Input.inputString.Length; i++)
                {
                    if (_keyInputJudger.IsKeyInputCorrect(Input.inputString[i], _charIndex, _questionCharList))
                    {
                        _charIndex++;
                        SoundManager.PlaySE("Key");
                        if (_questionCharList[_charIndex] == '@') // �u@�v���^�C�s���O�̏I���̔���ƂȂ�B
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