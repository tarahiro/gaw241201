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
    public class MonitorViewItemProvider
    {
        [Inject] CmdMonitorView _cmd;
        [Inject] SettingMonitorView _setting;

        public IMonitorViewItem Create(string bodyId)
        {
            switch (bodyId)
            {
                case "Cmd": return _cmd;
                case "Setting": return _cmd;

                default: return null;

            }

        }
    }
}