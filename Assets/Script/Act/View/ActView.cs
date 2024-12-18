using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201.View
{
    public class ActView : MonoBehaviour
    {
        const float _interval = 100f;

        [SerializeField] Transform _root;
        [SerializeField] WaveView _waveViewPrefab;
        [SerializeField] RestrictedCharView _restrictedCharViewPrefab;

        List<WaveView> _waveViewList;
        List<RestrictedCharView> _restrictedCharViewList;

        public void Enter(List<ActViewArgs> _args)
        {
            _waveViewList = new List<WaveView>();
            _restrictedCharViewList = new List<RestrictedCharView>();

            float virtualCursorX = 0f;

            for(int i = 0; i < _args.Count; i++)
            {
                if(_args[i].RestrictedCharList.Count > 0)
                {
                    RestrictedCharView charView = Instantiate(_restrictedCharViewPrefab, _root);
                    charView.GetComponent<RectTransform>().anchoredPosition = Vector2.right * virtualCursorX;
                    charView.SetText(_args[i].RestrictedCharList);
                    _restrictedCharViewList.Add(charView);
                }

                for (int j = 0; j < _args[i].WaveCount; j++)
                {
                    WaveView waveView = Instantiate(_waveViewPrefab, _root);
                    waveView.GetComponent<RectTransform>().anchoredPosition = Vector2.right * virtualCursorX;
                    _waveViewList.Add(waveView);
                    virtualCursorX += _interval;
                }

            }

            _root.localPosition = Vector2.left * virtualCursorX * .5f;
        }

        public void ClearWave()
        {
            for(int i = 0; i < _waveViewList.Count; i++)
            {
                if (!_waveViewList[i].IsCleared)
                {
                    _waveViewList[i].WaveClear();
                    break;
                }
            }
        }
    }
}