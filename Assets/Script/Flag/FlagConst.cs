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
        public enum Key
        {
            ApplicationTime,
            InputTime,
            Name,
            NameLower,
            BirthDate,
            IsSaveDataExist,
        }

        public enum RegisterOrder
        {
            ReadTime
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

                default:
                    Log.DebugAssert(key + "の初期値が指定されていません");
                    return "Null";
            }
        }
    }
}