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
using MessagePipe;

namespace Tarahiro.Ui
{
    [Routes]
    public partial class TranslationTextView : MonoBehaviour, ITranslationTextDisplayer
    {
        [SerializeField] TextMeshProUGUI tmp;
        [SerializeField] List<TMP_FontAsset> font;
        [SerializeField] List<float> fontSizeCoeffFromJp;

        ISubscriber<int> _subscriber;

        int _languageIndex = 0;
        float _initialFontSize;


        [Inject]public void Construct(ISubscriber<int> subscriber)
        {
            _subscriber = subscriber;

            _subscriber.Subscribe(x => SetLanguage(x));
            _initialFontSize = tmp.fontSize;
        }

        void Awake()
        {
        }

        public void SetLanguage(int languageIndex)
        {
            Log.Comment("ViewがSetLanguage : " + languageIndex);
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
