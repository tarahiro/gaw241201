using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Tarahiro;
using VContainer;
using VContainer.Unity;
using UniRx;
using System.Threading;
using gaw241201.View;
using UnityEngine.SceneManagement;

namespace gaw241201
{
    public class EndGameModel : IFlowModel
    {
        Subject<EndGameConst.Key> _entered = new Subject<EndGameConst.Key> ();
        public IObservable<EndGameConst.Key> Entered => _entered;
        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "ŠJŽn");
            _entered.OnNext(EnumUtil.KeyToType<EndGameConst.Key>(bodyId));
        }

        public void Exit()
        {
            SceneManager.LoadScene("Main");
        }

#if ENABLE_DEBUG
        public void ForceEndFlow()
        {
        }
#endif
    }
}