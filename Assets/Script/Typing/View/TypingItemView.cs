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
        [Inject] IKeyInputJudger _keyInputJudger;
        [Inject] IQuestionTextGenerator _questionTextGenerator;
        [Inject] IGazable _gazable;
        TypingViewArgs _viewArgsList;

        [SerializeField] private TextMeshProUGUI _tmpSample; // �����ɓ��{��\����TextMeshPro���A�^�b�`����B
        [SerializeField] private TextMeshProUGUI _tmpQuestion; // �����Ƀ��[�}���\����TextMeshPro���A�^�b�`����B

        private readonly List<char> _questionCharList = new List<char>();

        private int _charIndex;
        bool _isWindows = true;
        bool _isMac = false;
        bool _isEndLoop = false;


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
            _tmpSample.text = "";
            _tmpQuestion.text = "";
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
            _tmpSample.text = _viewArgsList.SampleText;
            
            //���e�L�X�g�𔽉f
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