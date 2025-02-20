using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using MessagePipe;
using VContainer.Unity;
using Tarahiro.MasterData;

namespace Tarahiro.Ui
{
    public class EmbeddedTextPresenter : IPostInitializable
    {
        [Inject] ILanguageMessageMasterDataProvider _provider;
        [Inject] EmbeddedTextViewManager _viewManager;
        [Inject] ISubscriber<int> _subscriber;

        CompositeDisposable _disposable = new CompositeDisposable();



        public void PostInitialize()
        {
            _viewManager.Finded.Subscribe(OnFind).AddTo(_disposable);

            _viewManager.Initialize();
        }

        void OnFind(EmbeddedTranslationTextView findedView)
        {
            findedView.Construct(_subscriber);

            var master = _provider.TryGetFromId(findedView.Id);
            if(master != null)
            {
                findedView.SetTranslatableText(master.GetMaster().Message);
            }
            else
            {
                Log.DebugAssert(findedView.Id + "がマスターに存在しません");
            }
        }
    }
}
