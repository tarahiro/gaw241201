using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public static class ViewUtil
    {
        public static void SetFullScreenPathRenderer(ViewConst.Renderer renderer)
        {
            Camera.main.GetUniversalAdditionalCameraData().SetRenderer((int)renderer);
        }
    }
}