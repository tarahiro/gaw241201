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
            Log.Comment("Message“à‚ÌKey‚ÌŒŸõŠJn");
            string returnMessage = message;

            foreach(FlagConst.MessageKey key in Enum.GetValues(typeof(FlagConst.MessageKey))) 
            {
                string command = "<key=" + key.ToString()  + ">";
                Log.Comment(command);
                if (returnMessage.Contains(command))
                {
                    returnMessage = returnMessage.Replace(command, _replacerProvider.GetKeyReplacer(key).ReplaceTo(key));
                }

            }

            return returnMessage;
        }

       

        
    }
}