using Cysharp.Threading.Tasks;
using MessagePipe;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using static gaw241201.SettingRootHundler;

namespace gaw241201
{
    public class SettingExitMonitorModel
    {
        FlowSwitchPublisher _publisher;



        public void OnChangeFlagsBySetting(List<MoniteredChanged> list)
        {
            foreach (var moniteredChanged in list)
            {
                switch (moniteredChanged.Key)
                {
                    case FlagConst.Key.IsRoguelikeEnabled:
                        if(moniteredChanged.NowValue == Tarahiro.Const.c_true)
                        {
                            _publisher.Publish(new FlowSwitchArgs_Fake(FlowMasterConst.FlowMasterLabel.ExhibitionForestRoguelikeFlow, ""));
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }
}