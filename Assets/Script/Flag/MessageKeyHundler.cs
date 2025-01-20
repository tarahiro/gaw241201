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
    public class MessageKeyHundler
    {
        [Inject] MessageKeyReplacerProvider _replacerProvider;

        public string HundleKey(string message)
        {
            Log.Comment("Message内のKeyの検索開始");
            string returnMessage = message;

            foreach(FlagConst.MessageKey key in Enum.GetValues(typeof(FlagConst.MessageKey))) 
            {
                string command = "<key=" + key.ToString()  + ">";
                if (returnMessage.Contains(command))
                {
                    returnMessage = returnMessage.Replace(command, _replacerProvider.GetKeyReplacer(key).ReplaceTo(key));
                }

            }

            return returnMessage;
        }

        //将来的にクラス分けるかも？
        public ITranslatableText HundleKeyToTranslatableText(ITranslatableText text)
        {
            List<string> TextList = new List<string>();

            for(int i = 0; i < LanguageConst.AvailableLanguageNumber; i++)
            {
                TextList.Add(HundleKey(text.GetTranslatedText(i)));
            }

            return new TranslatableText(TextList.ToArray());
        }

       

        
    }
}