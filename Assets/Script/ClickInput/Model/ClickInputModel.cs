using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;

namespace gaw241201
{
    public class ClickInputModel : IFlowModel
    {
        // 何らかのボタンをクリックしたら処理を戻す
        public async UniTask EnterFlow(string bodyId)
        {
            Log.Comment("ClickInputModel開始");
        }
#if ENABLE_DEBUG
        public void ForceEndFlow()
        {

        }
        public string ForceGetCategory => "ClickInput";
#endif
    }
}