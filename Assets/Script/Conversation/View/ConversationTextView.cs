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
            await TextUtil.DisplayTextByCharacter(text.GetTranslatedText(_translationTextDisplayer.GetLanguageIndex()), _tmp, "Talk", _decideKeys , ct,false);
            await UniTask.Yield(PlayerLoopTiming.Update,ct);
            _cursor.StartBlink();
            SetCursorPosition();
            await UniTask.WaitUntil(() => _decideKeys.Any(x => Input.GetKeyDown(x)) , cancellationToken:ct);
            _cursor.StopBlink();
            _cursor.EraseCursor();
        }

        void SetCursorPosition()
        {
            _cursor.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (_tmp.preferredWidth * .5f + c_interval);
        }
    }
}