using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gaw241201.View
{
    public class ConversationViewArgs
    {
        public string Message { get; set; }
        public string Facial { get; set; }

        public ConversationViewArgs(string message, string facial)
        {
            Message = message;
            Facial = facial;
        }
    }
}