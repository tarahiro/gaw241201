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
    public static class FlagConst
    {
        [System.Serializable]
        public enum Key
        {
            ApplicationTime,
            InputTime,
            Name,
            NameLower,
            BirthDate,
            IsSaveDataExist,
            IsRoguelikeEnabled,
            RestartFlow,

            IsSettingEnable,
            OnEnterSettingConversation,
            IsAdvancedSettingEnabled,
        }

        public enum RegisterOrder
        {
            ReadTime,
            SettingEnable,
        }

        public enum MessageKey
        {

            ApplicationTime,
            DiffSecond,
            Name,
            NameLower,
            Device,
            DeviceLower,
            DeviceModel,
            DeviceModelLower,
            DeviceType,
            GraphicsDeciveName,
            GraphicsDeviceType,
            GraphicsDeviceVendor,
            GraphicsDeviceVersion
        }

        public enum ContainableMasterKey
        {
            Leet,
            Word
        }

        public static string InitialValue(Key key)
        {
            switch (key)
            {
                case Key.ApplicationTime: return "000000";
                case Key.InputTime: return "000000";
                case Key.Name: return "PLAYER";
                case Key.NameLower: return "player";
                case Key.BirthDate: return "BirthDate";
                case Key.IsSaveDataExist: return Tarahiro.Const.c_false;
                case Key.IsRoguelikeEnabled: return Tarahiro.Const.c_false;
                case Key.RestartFlow: return "ExhibitionMainFlow";
                case Key.IsSettingEnable: return Tarahiro.Const.c_false;
                case Key.OnEnterSettingConversation: return "";
                case Key.IsAdvancedSettingEnabled: return Tarahiro.Const.c_false;

                default:
                    Log.DebugAssert(key + "ÇÃèâä˙ílÇ™éwíËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
                    return "Null";
            }
        }

        public const int c_NameMaxLength = 12;
        public const int c_NameMinLength = 2;

        public const int c_TimeMaxLength = 6;
        public const int c_BirthMaxLength = 8;
    }
}