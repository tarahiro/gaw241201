using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        private void Start()
        {
            _tmp = GetComponent<TextMeshProUGUI>();
        }

        public async UniTask Enter(string text,CancellationToken ct)
        {
            _tmp.text = "";
            await TextUtil.DisplayTextByCharacter(text, _tmp, "Text", KeyCode.Space, ct);
            await UniTask.Yield(PlayerLoopTiming.Update);
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Space), cancellationToken:ct);
        }
    }
}