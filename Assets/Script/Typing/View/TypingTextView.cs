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
    public class TypingTextView : MonoBehaviour
    {
        [Inject] IGazable _gazable;
        [SerializeField] private TextMeshProUGUI _tmpSample; 
        [SerializeField] private TextMeshProUGUI _tmpQuestion; 

        public void ResetText()
        {
            _tmpSample.text = "";
            _tmpQuestion.text = "";

        }

        public void SetSampleText(string s)
        {
            _tmpSample.text = s;
        }
        public void SetQuestionText(string s,int _charIndex, int _charListCount)
        {
            _tmpQuestion.text = s;
            _gazable.Gaze((Vector2)Camera.main.WorldToScreenPoint(_tmpQuestion.transform.position) +
    Vector2.right * _tmpQuestion.preferredWidth * (-0.5f + ((float)_charIndex) / _charListCount));
        }

        public void SetQuestionText(string s)
        {
            _tmpQuestion.text = s;
        }
    }
}