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
    public class MessageKeyReplacerProvider
    {
        [Inject] ApplicationTimeKeyReplacer _applicationTimeKeyReplacer;
        [Inject] DiffSecondKeyReplacer _diffSecondKeyReplacer;
        [Inject] RowKeyReplacer _rowKeyReplacer;
        public IKeyReplacer GetKeyReplacer(ConversationConst.Key key)
        {
            switch (key)
            {
                case ConversationConst.Key.ApplicationTime: 
                    return _applicationTimeKeyReplacer;

                case ConversationConst.Key.DiffSecond:
                    return _diffSecondKeyReplacer;

                default:
                    return _rowKeyReplacer;
            }
    
        }
    }
}