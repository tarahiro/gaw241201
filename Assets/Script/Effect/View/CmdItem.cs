using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class CmdItem : MonoBehaviour, IEffectItemView
    {
        [SerializeField] string _text;
        [SerializeField] CmdLine _cmdLinePrefab;
        [SerializeField] Transform _lineRoot;


        public bool IsAutoEnd => false;
        CmdLine _cmdLine;

        const float c_showTime = 2f;

        public async UniTask Enter(CancellationToken cancellationToken)
        {
            _cmdLine = Instantiate(_cmdLinePrefab, _lineRoot);
            _cmdLine.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            _cmdLine.Construct(_text);
            await UniTask.WaitForSeconds(c_showTime);
            _cmdLine.SetLine();
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
            _cmdLine.Unfoucus();
        }   

    }
}