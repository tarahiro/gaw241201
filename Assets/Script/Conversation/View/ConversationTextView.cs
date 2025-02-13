using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using Tarahiro;
using Tarahiro.Ui;
using TMPro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using MessagePipe;

namespace gaw241201.View
{
    public class ConversationTextView : MonoBehaviour
    {
        TextMeshProUGUI _tmp;
        ITranslationTextDisplayer _translationTextDisplayer;

        [SerializeField] BlinkableCursor _cursor;

        const float c_interval = 10f;

        KeyCode[] _decideKeys = new KeyCode[]
        {
            KeyCode.Return,
            KeyCode.Space,
        };


        private void Start()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
            _translationTextDisplayer = GetComponent<ITranslationTextDisplayer>();
        }

        public async UniTask Enter(ITranslatableText text,CancellationToken ct)
        {
            _tmp.text = "";
            SoundManager.PlaySE("TalkShort");

            _publisher.PublishActiveLayer(_layer);

            await TextUtil.DisplayTextByCharacter(text.GetTranslatedText(_translationTextDisplayer.GetLanguageIndex()), _tmp, "Talk", _decideKeys , ct,false);

            await UniTask.Yield(PlayerLoopTiming.Update,ct);
            _cursor.StartBlink();
            SetCursorPosition();
            await UniTask.WaitUntil(() => !_isBlocked() && _decideKeys.Any(x => Input.GetKeyDown(x)) , cancellationToken:ct);
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
            _cursor.StopBlink();
            _cursor.EraseCursor();
            _publisher.ResetActiveLayer();

        }

        void SetCursorPosition()
        {
            _cursor.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (_tmp.preferredWidth * .5f + c_interval);
        }


        ISubscriber<ActiveLayerConst.InputLayer> _subscriber;
        ActiveLayerPublisher _publisher;

        [Inject]
        public void Construct(ISubscriber<ActiveLayerConst.InputLayer> subscriber, ActiveLayerPublisher publisher)
        {
            _subscriber = subscriber;
            _publisher = publisher;

            _subscriber.Subscribe(OnActiveLayerChanged);
        }


        ActiveLayerConst.InputLayer _activeLayer;
        [SerializeField] ActiveLayerConst.InputLayer _layer;

        bool _isBlocked()
        {
            return _layer < _activeLayer;
        }

        void OnActiveLayerChanged(ActiveLayerConst.InputLayer layer)
        {
            _activeLayer = layer;
        }

    }
}