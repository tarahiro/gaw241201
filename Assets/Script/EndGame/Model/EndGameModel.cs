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
using MessagePipe;

namespace gaw241201
{
    public class EndGameModel : ICategoryEnterableModel
    {
        [Inject] ISavable _savable;
        [Inject] ScenePublisher _publisher;
        [Inject] ICancellationTokenPure _cancellationTokenSource;

        Subject<EndGameArgs> _entered = new Subject<EndGameArgs> ();
        public IObservable<EndGameArgs> Entered => _entered;


        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "ŠJŽn");
            //_savable.Save();
            _cancellationTokenSource.SetNew();
            _entered.OnNext(new EndGameArgs(_cancellationTokenSource.Token, EnumUtil.KeyToType<EndGameConst.Key>(bodyId)));
        }

        public void Restart()
        {
            GlobalStaticFlag.IsSkipTitle = true;
            _publisher.Publish();
            SceneManager.LoadScene("Main");
        }

        public void ToTitle()
        {
            GlobalStaticFlag.IsSkipTitle = false;
            _publisher.Publish();
            SceneManager.LoadScene("Main");
        }

        public void ForceEndFlow()
        {
        }

    }
}