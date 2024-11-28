using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Tarahiro.Ui
{
    public static class TextUtil
    {
        const float c_defaultTextIntervalTime = .1f;
        public static async UniTask DisplayTextByCharacter(string text, TextMeshProUGUI textMeshProUGUI, string SeLabel,KeyCode decide, CancellationToken ct, float textIntervalTime = c_defaultTextIntervalTime)
        {
            var t = new GameObject().AddComponent<TextCounter>();
            t.StartTextCount(text,textMeshProUGUI,SeLabel,decide,c_defaultTextIntervalTime,ct);
            await UniTask.WaitUntil(() => t.IsEndTextCount, cancellationToken: ct);
            GameObject.Destroy(t.gameObject);
        }
    }
}