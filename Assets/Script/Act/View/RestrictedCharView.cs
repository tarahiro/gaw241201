using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using TMPro;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class RestrictedCharView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _tmp;
        public void SetText(List<char> charList)
        {
            _tmp.text = "";

            for(int i = 0; i < charList.Count; i++)
            {
                _tmp.text += charList[i] + " ";
            }
        }
    }
}