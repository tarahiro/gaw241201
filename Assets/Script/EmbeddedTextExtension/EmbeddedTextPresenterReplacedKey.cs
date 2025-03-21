using Cysharp.Threading.Tasks;
using gaw241201.Model;
using gaw241201.View;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.Ui;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using MessagePipe;

//アセンブリはFlagとは別にすべきな気もするが…
namespace gaw241201
{
    public class EmbeddedTextPresenterReplacedKey : IEmbeddedTextPresenter
    {
        [Inject] ILanguageMessageMasterDataProvider _provider;
        [Inject] EmbeddedTextViewManager _viewManager;
        [Inject] ISubscriber<int> _subscriber;
        [Inject] MessageKeyHundler _messageKeyHundler;

        CompositeDisposable _disposable = new CompositeDisposable();



        public void PostInitialize()
        {
            _viewManager.Finded.Subscribe(OnFind).AddTo(_disposable);

            _viewManager.Initialize();
        }

        public void OnFind(EmbeddedTranslationTextView findedView)
        {
            findedView.Construct(_subscriber);

            var master = _provider.TryGetFromId(findedView.Id);
            if (master != null)
            {
                findedView.SetTranslatableText(new TranslationTextKeyReplacable(_messageKeyHundler, master.GetMaster().Message));
            }
        }
    }
}