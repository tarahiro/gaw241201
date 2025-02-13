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
        ISavable _savable;
        ISubscriber<Unit> _subscriber;
        ScenePublisher _publisher;

        Subject<EndGameArgs> _entered = new Subject<EndGameArgs> ();
        public IObservable<EndGameArgs> Entered => _entered;

        CancellationTokenSource _cancellationTokenSource;

        [Inject]
        public EndGameModel(ISavable savable, ISubscriber<Unit> subscriber, ScenePublisher publisher)
        {
            _savable = savable;
            _subscriber = subscriber;
            _publisher = publisher;
            _subscriber.Subscribe(OnSceneEnded);
        }

        public async UniTask EnterFlow(string bodyId)
        {
            Log.DebugLog(bodyId + "ŠJŽn");
            //_savable.Save();
            _cancellationTokenSource = new CancellationTokenSource();
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

        void OnSceneEnded(Unit unit)
        {
            Log.DebugLog("OnSceneEnd");
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }
    }
}