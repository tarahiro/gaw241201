using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.PlayerLoop;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class TextHighlighter : MonoBehaviour, ITextHighlighter
    {
        [SerializeField] private float sclAmp;
        [SerializeField] private float sclChangeTime;
        [SerializeField] private float sclWaitTime;
        ITextScaleChanger _textScaleChanger;

        float[] time = new float[100];
        bool[] _isEnableList = new bool[100];

        public void Construct(ITextScaleChanger textScaleChanger)
        {
            _textScaleChanger = textScaleChanger;
        }

        public void StartHighlight(int textIndex)
        { 
            time[textIndex] = 0;
            _isEnableList[textIndex] = true;
        }

        public void StopHighlight(int textIndex)
        {
            time[textIndex] = 0;
            _isEnableList[textIndex] = false;
        }

        public TMP_TextInfo Tick(TMP_TextInfo tmpInfo)
        {
            int count = Mathf.Min(tmpInfo.characterCount, tmpInfo.characterInfo.Length, _isEnableList.Length);
            for (int i = 0; i < count; i++)
            {
                if (_isEnableList[i])
                {
                    time[i] += Time.deltaTime;
                    float t = time[i];

                    if (t < sclChangeTime)
                    {

                       tmpInfo =  _textScaleChanger.TextScaleChange(tmpInfo, i, new Vector2(1f + sclAmp * .2f * (t / sclChangeTime), (1f + sclAmp * (t / sclChangeTime))));
                    }
                    else if (t < sclWaitTime + sclChangeTime)
                    {
                        tmpInfo = _textScaleChanger.TextScaleChange(tmpInfo,i, new Vector2(1f + sclAmp * .2f, 1f + sclAmp));
                    }
                    else if (t < sclChangeTime * 2f + sclWaitTime)
                    {
                        tmpInfo = _textScaleChanger.TextScaleChange(tmpInfo,i, new Vector2(1f + sclAmp * .2f * ((sclChangeTime * 2f + sclWaitTime - t) / sclChangeTime), 1f + sclAmp * ((sclChangeTime * 2f + sclWaitTime - t) / sclChangeTime)));
                    }
                    else
                    {
                        _isEnableList[i] = false;
                    }
                }
            }
            return tmpInfo;
        }
        
    }
}