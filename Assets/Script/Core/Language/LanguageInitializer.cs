using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201
{
    public class LanguageInitializer : IStartable
    {
        [Inject] LanguageModel _languageModel;

        //�����I�ɂ�Steam�̌���ݒ�Ȃǂ������Ă���
        LanguageConst.AvailableLanguage _initialLanguage = LanguageConst.AvailableLanguage.Japanese;

        public void Start()
        {
            _languageModel.SetLanguage(_initialLanguage);
        }
    }
}