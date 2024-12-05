using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class GlitchEffectItemView : MonoBehaviour, IEffectItemView 
    {

        [SerializeField] bool _isAutoEnd;
        [SerializeField] ViewConst.Renderer _renderer;


        public bool IsAutoEnd => _isAutoEnd;


        public virtual async UniTask Enter(CancellationToken cancellationToken)
        {
            ViewUtil.SetFullScreenPathRenderer(_renderer);
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
            ViewUtil.SetFullScreenPathRenderer(ViewConst.Renderer.Default);
        }
    }
}