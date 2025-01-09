using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class StageBgDepthObject : MonoBehaviour
    {
        float _alpha = 0f;

        List<SpriteRenderer> _renderer = new List<SpriteRenderer>();    


        public void Initialize()
        {
            RegisterRenderer(transform, _renderer);
        }

        public void RegisterRenderer(Transform t, List<SpriteRenderer> rendererList)
        {
            if (t.GetComponent<SpriteRenderer>() != null)
            {
                rendererList.Add(t.GetComponent<SpriteRenderer>());
            }

            for (int i = 0; i < t.childCount; i++)
            {
                RegisterRenderer(t.GetChild(i), rendererList);
            }
        }

        public void SetAlpha(float alpha)
        {
            _alpha = alpha;
            foreach(var renderer in _renderer)
            {
                Color c = renderer.color;
                c.a = alpha;
                renderer.color = c;
            }
        }


        const float c_nontranparentDepth = 5f;
        const float c_completeTransparentDepth = 10f;

        private void Update()
        {
            float depth = transform.position.z - Camera.main.transform.position.z; float alpha;
            if (depth < c_nontranparentDepth)
            {
                alpha = 1f;
            }
            else if (depth < c_completeTransparentDepth)
            {
                alpha = 1f - (depth - c_nontranparentDepth) / (c_completeTransparentDepth - c_nontranparentDepth);
            }
            else
            {
                alpha = 0;
            }
            SetAlpha(alpha);
        }

    }
}