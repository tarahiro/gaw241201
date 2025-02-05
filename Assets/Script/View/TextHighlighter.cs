using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
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
            Log.Comment("テキストハイライト開始");
            time[textIndex] = 0;
            _isEnableList[textIndex] = true;
        }

        public void StopHighlight(int textIndex)
        {
            time[textIndex] = 0;
            _isEnableList[textIndex] = false;
        }

        public void Tick()
        {
            for (int i = 0; i < _isEnableList.Length; i++)
            {
                if (_isEnableList[i])
                {
                    time[i] += Time.deltaTime;
                    float t = time[i];

                    if (t < sclChangeTime)
                    {

                        _textScaleChanger.TextScaleChange(i, new Vector2(1f + sclAmp * .2f * (t / sclChangeTime), (1f + sclAmp * (t / sclChangeTime))));
                    }
                    else if (t < sclWaitTime + sclChangeTime)
                    {
                        _textScaleChanger.TextScaleChange(i, new Vector2(1f + sclAmp * .2f, 1f + sclAmp));
                    }
                    else if (t < sclChangeTime * 2f + sclWaitTime)
                    {
                        _textScaleChanger.TextScaleChange(i, new Vector2(1f + sclAmp * .2f * ((sclChangeTime * 2f + sclWaitTime - t) / sclChangeTime), 1f + sclAmp * ((sclChangeTime * 2f + sclWaitTime - t) / sclChangeTime)));
                    }
                    else
                    {
                        _isEnableList[i] = false;
                    }
                }
            }
        }
        
    }
}