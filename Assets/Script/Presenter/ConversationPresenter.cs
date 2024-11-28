using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
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

        CompositeDisposable _disposable = new CompositeDisposable();

        public void PostInitialize()
        {
            _model.Entered.Subscribe(Fake).AddTo(_disposable);
        }

        void Fake(IConversationMaster _master)
        {
            Log.DebugLog(_master.Id);
        }
    }
}