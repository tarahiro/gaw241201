using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using TMPro;

namespace gaw241201.View
{
    public class CmdLine : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _tmp;
        [SerializeField] BlinkableCursor _cursor;

        const float c_cursorInterval = 20f;
        string _text;

        public void Construct(string text)
        {
            _text = text;
        }

        private void Start()
        {
            _tmp.text = "";
            SetCursorPosition();
            _cursor.StartBlink();
        }

        public void SetLine()
        {
            _tmp.text = _text;
            SetCursorPosition();
        }

        public void Unfoucus()
        {
            _cursor.StopBlink();
            _cursor.EraseCursor();
        }

        void SetCursorPosition()
        {
            _cursor.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (_tmp.preferredWidth + c_cursorInterval);
        }

    }
}