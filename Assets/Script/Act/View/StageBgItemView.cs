using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using LitMotion;
using LitMotion.Extensions;

namespace gaw241201.View
{
    public class StageBgItemView : MonoBehaviour
    {
        const float c_initialMergin = 2.6f;
        const float c_interval = 0.7f;

        ActBgViewArgs _args;
        float f = 0;

        [SerializeField] Transform _depthObjectRoot;

        [SerializeField] StageBgDepthObject _normalDepthObject;
        [SerializeField] StageBgDepthObject _bossDepthObject;

        [SerializeField] Color _bgColor;


        public StageBgItemView Construct(ActBgViewArgs args)
        {
            _args = args;
            Log.Comment(args.BodyId + "‚ÌItemView‚ðConstruct");
            return this;
        }

        public void Initialize()
        {
            transform.position = new Vector3(0f,0f,Camera.main.transform.position.z);
            Camera.main.backgroundColor = _bgColor;

            Log.DebugLog("View‚ÌWaveNumber: "+_args.WaveNumber);

            for (int i = 0; i < _args.WaveNumber; i++)
            {
                var obj = Instantiate(_normalDepthObject, _depthObjectRoot);
                obj.Initialize();
                SetDepth(obj, c_initialMergin + c_interval * i);
            }
        }

        void SetDepth(StageBgDepthObject depthObject, float depth)
        {
            depthObject.transform.position = transform.position + Vector3.forward * depth;
        }

        const float c_walkTime = .5f;
        const float c_walkShakeHeight = .02f;

        public void ToNext()
        {
            LMotion.Create(_depthObjectRoot.transform.localPosition.z, _depthObjectRoot.transform.localPosition.z - c_interval, c_walkTime).BindToLocalPositionZ(_depthObjectRoot);
            LMotion.Create(transform.position.y, transform.position.y + c_walkShakeHeight, c_walkTime / 4f)
            .WithLoops(4, LoopType.Yoyo)
            .WithEase(Ease.InBack)
            .BindToLocalPositionY(transform);
        }
    }
}