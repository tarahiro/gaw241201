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
        const float c_glitchTime = 2f;
        UniversalAdditionalCameraData universalAdditionalCameraData;

        public bool IsAutoEnd => true;


        public async UniTask Enter(CancellationToken cancellationToken)
        {
            universalAdditionalCameraData = Camera.main.GetUniversalAdditionalCameraData();
            universalAdditionalCameraData.SetRenderer(1);

            await UniTask.WaitForSeconds(c_glitchTime, cancellationToken: cancellationToken);
            
        }

        public async UniTask End(CancellationToken cancellationToken)
        {
            universalAdditionalCameraData.SetRenderer(0);

        }
    }
}