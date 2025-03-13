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
    public class PlayerNameInputJudger : ICharInputJudger
    {
        FreeInputIndexer _indexer;

        public PlayerNameInputJudger(FreeInputIndexer indexer)
        {
            _indexer = indexer;
        }

        public bool IsCharAvailable(char c)
        {
            if (_indexer.IsFocusExist)
            {
                return char.IsLetterOrDigit(c);
            }
            else
            {
                return false;
            }
        }
    }
}