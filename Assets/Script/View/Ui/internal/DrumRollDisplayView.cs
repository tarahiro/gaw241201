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
    public class DrumRollDisplayView : MonoBehaviour
    {
        //‘I‘ð‚µ‚Ä‚È‚¢‚à‚Ì‚ÍŒ©‚¦‚È‚¢‘O’ñ

        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] GameObject _highlighter;

        List<string> _textList;

        private void Start()
        {
            _highlighter.SetActive(false);
        }

        public void Initialize(List<string> textList)
        {
            _textList = textList;
        }

        public void SetFocus(int index)
        {
            _text.text = _textList[index];
        }

        public void Enter()
        {
            _highlighter.SetActive(true);
        }

        public void Exit()
        {
            _highlighter.SetActive(false);
        }

    }
}