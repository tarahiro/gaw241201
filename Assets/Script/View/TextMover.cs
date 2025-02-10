using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TextMover : MonoBehaviour
    {
        IIdleTextMover _idleTextMover;
        ITextScaleChanger _textScaleChanger;
        ITextHighlighter _textHighlighter;
        TMP_Text _tmpText;

        void Start()
        {
            _idleTextMover = GetComponent<IIdleTextMover>();
            _textScaleChanger = GetComponent<ITextScaleChanger>();
            _textHighlighter = GetComponent<ITextHighlighter>();

            _tmpText = GetComponent<TMP_Text>();
            
            _idleTextMover.Construct(_tmpText, _textScaleChanger);
            _textHighlighter.Construct(_textScaleChanger);
            _textScaleChanger.Construct(_tmpText);

            _idleTextMover.Initialize();
            _textScaleChanger.Initialize();

            _idleTextMover.StartIdle();
        }


        float f = 0;

        private void Update()
        {
            _idleTextMover.Tick();
            _textHighlighter.Tick();
        }

        public void HighlightText(int textIndex)
        {
            _textHighlighter.StartHighlight(textIndex);
        }
        public void LowlightText(int textIndex)
        {
            _textHighlighter.StopHighlight(textIndex);
        }
    }
}