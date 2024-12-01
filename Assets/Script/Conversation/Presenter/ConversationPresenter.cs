using Cysharp.Threading.Tasks;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace gaw241201.Presenter
{
    public class ConversationPresenter : IPostInitializable
    {
        [Inject] ConversationModel _model;
        [Inject] ConversationView _view;
        [Inject] Func<IConversationMaster, ConversationViewArgs> _factory;

        CompositeDisposable _disposable = new CompositeDisposable();
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(x =>  _view.EnterConversation(_factory.Invoke(x), cancellationTokenSource.Token).Forget()).AddTo(_disposable);
            _view.Completed.Subscribe(x => _model.EndSIngleConversation()).AddTo(_disposable);
#if ENABLE_DEBUG
            _model.ForceEnded.Subscribe(_ => Cancell()).AddTo(_disposable);
#endif
        }

#if ENABLE_DEBUG
        void Cancell()
        {
            Log.Comment("ConversationPresenterのキャンセル開始");

            cancellationTokenSource.Cancel();
            cancellationTokenSource = new CancellationTokenSource();
        }
#endif
    }
}