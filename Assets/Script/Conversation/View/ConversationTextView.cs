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
        }

        public async UniTask Enter(string text,CancellationToken ct)
        {
            _tmp.text = "";
            SoundManager.PlaySE("TalkShort");
            Log.Comment("TextView表示開始");
            await TextUtil.DisplayTextByCharacter(text, _tmp, "Talk", _decideKeys , ct,false);
            await UniTask.Yield(PlayerLoopTiming.Update,ct);
            Log.Comment("TextView表示終了");
            _cursor.StartBlink();
            SetCursorPosition();
            await UniTask.WaitUntil(() => _decideKeys.Any(x => Input.GetKeyDown(x)) , cancellationToken:ct);
            _cursor.StopBlink();
            _cursor.EraseCursor();
            Log.Comment("TextView決定終了");
        }

        void SetCursorPosition()
        {
            _cursor.GetComponent<RectTransform>().anchoredPosition = Vector2.right * (_tmp.preferredWidth * .5f + c_interval);
        }
    }
}