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
    public static class ActiveLayerConst
    {

        public enum InputLayer
        {
            None = 0,
            FreeInput = 5,
            Typing = 10,
            Conversation = 20,
            Skill = 30,
            SettingMenu = 100,
            SettingMenuItem = 200,
            SettingConversation = 300,
        
            GameOver = 1000
        }
    }
}