using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class SkillTagView : MonoBehaviour
    {
        const float c_mergin = 5f;

        [SerializeField] Image _image;
        [SerializeField] TextMeshProUGUI _text;


        public void Set(string text)
        {
            _text.text = text;
            _image.rectTransform.sizeDelta = new Vector2(_text.preferredWidth + c_mergin * 2f, _text.preferredHeight);
        }
        public float Width()
        {
            return _image.rectTransform.sizeDelta.x;
        }
    }
}