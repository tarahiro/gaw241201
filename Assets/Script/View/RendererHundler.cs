using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tarahiro;
using UniRx;
using Unity.Plastic.Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class RendererHundler : MonoBehaviour
    {
        [SerializeField] Renderer2DData _renderer2DData;

        readonly RendererFeature[] _defaultFeatures = new RendererFeature[]
        {
            RendererFeature.ChromaticAberration
        };

        private void OnApplicationQuit()
        {
            foreach(RendererFeature feature in Enum.GetValues(typeof(RendererFeature)))
            {
                if (_defaultFeatures.Contains(feature))
                {
                    ActivateRendererFeature(feature.ToString());
                }
                else
                {
                    DeactivateRendererFeature(feature.ToString());
                }
            }
        }

        public void ActivateRendererFeature(RendererFeature feature) => ActivateRendererFeature(feature.ToString());
        public void DeactivateRendererFeature(RendererFeature feature) => DeactivateRendererFeature(feature.ToString());

        void ActivateRendererFeature(string featureName)
        {
            _renderer2DData.rendererFeatures.Find(x => x.name == featureName).SetActive(true);
        }

        void DeactivateRendererFeature(string featureName)
        {
            _renderer2DData.rendererFeatures.Find(x => x.name == featureName).SetActive(false);
        }

        public enum RendererFeature
        {
            ChromaticAberration,
            ScanLine,
            GlitchLarge,
            GlitchSmall
        }
    }
}