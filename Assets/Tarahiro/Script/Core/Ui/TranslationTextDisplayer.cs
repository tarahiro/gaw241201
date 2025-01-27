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
    public partial class TranslationTextView : MonoBehaviour, ITranslationTextDisplayer
    {
        [SerializeField] TextMeshProUGUI tmp;
        [SerializeField] List<TMP_FontAsset> font;
        [SerializeField] List<float> fontSizeCoeffFromJp;

        int _languageIndex = 0;
        float _initialFontSize;

        void Awake()
        {
            _initialFontSize = tmp.fontSize;
        }

        public void SetLanguage(int languageIndex)
        {
            _languageIndex = languageIndex;
            tmp.font = font[_languageIndex];
            tmp.fontSize = _initialFontSize * fontSizeCoeffFromJp[_languageIndex];
        }

        public int GetLanguageIndex()
        {
            Log.DebugAssert(_languageIndex >= 0);
            return _languageIndex;
        }

        public void OnChangeLanguage(NotifyLanguageCommand cmd)
        {
            Log.Comment("コマンド受け取り");
            SetLanguage(cmd.LanguageIndex);
        }
    }
}
