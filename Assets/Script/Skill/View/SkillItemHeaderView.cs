using Cysharp.Threading.Tasks;
using PlasticGui.WebApi.Responses;
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
    public class SkillItemHeaderView : MonoBehaviour
    {
        const float c_mergin = 5f; 

        [SerializeField] TextMeshProUGUI _name;
        [SerializeField] SkillTagView _tagView;

        public void Set(string name,string category)
        {
            _name.text = name;
            _tagView.Set(category);

            float length = 0;
            length += _name.preferredWidth;
            length += c_mergin;
            length += _tagView.Width();

            _name.rectTransform.sizeDelta = new Vector2(_name.preferredWidth, _name.preferredHeight);

            _name.rectTransform.localPosition = new Vector2(length * (-.5f) + _name.rectTransform.sizeDelta.x * .5f, 0);
            _tagView.transform.localPosition = new Vector2(length * .5f - _tagView.Width() * .5f, 0);
        }
    }
}