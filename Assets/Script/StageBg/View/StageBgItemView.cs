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
        const float c_interval = 1f;

        StageBgViewArgs _args;
        float f = 0;

        [SerializeField] Transform _depthObjectRoot;

        [SerializeField] StageBgDepthObject _normalDepthObject;
        [SerializeField] StageBgDepthObject _bossDepthObject;


        public StageBgItemView Construct(StageBgViewArgs args)
        {
            _args = args;
            Log.Comment(args.BodyId + "��ItemView��Construct");
            return this;
        }

        public void Initialize()
        {
            transform.position = new Vector3(0f,0f,Camera.main.transform.position.z);

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