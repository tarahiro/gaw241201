using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class WaveView : MonoBehaviour
    {
        [SerializeField] Image _image;

        public bool IsCleared { get; private set; } =  false;

        public void WaveClear()
        {
            _image.color = Color.gray;
            IsCleared = true;
        }
    }
}