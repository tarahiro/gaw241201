using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using TMPro;
using VContainer;
using VitalRouter;

namespace Tarahiro.Ui
{
    [Routes]
    public partial class EmbeddedTranslationTextView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI tmp;
        [SerializeField] string Id;

        public void OnChangeLanguage(NotifyLanguageCommand cmd)
        {
            Log.Comment("EmbeddedTranslationTextView : コマンド受け取り");
            if(cmd.TryGetMessage(Id, out var message))
            {
                tmp.text = message;
            }
        }
    }
}
