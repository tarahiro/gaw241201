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
    public static class FlowMasterConst
    {
        [System.Serializable]
        public enum FlowMasterLabel
        {
            HorrorStoryMainFlow,
            TrueEndFlow,
            SaveDataExistFlow,
            ScreenShotFlow,
            TypingTestFlow,
            FreeInputTestFlow,
            TypingRoguelikeMainFlow,

            RealLeftEyeFlow,
            GoatEyeFlow,
            CompanyFlow,
            WishlistFlow,
            GameoverFlow,
            GameoverByEyeFlow,
            GameoverByEyeWithAnimalFlow,
            GameoverByEyeWithLightFlow,
            KillTestFlow,
            ExhibitionMainFlow,
            ExhibitionForestFlow,
            ExhibitionForest2Flow,
            ExhibitionForestRestartFlow,
            ExhibitionForest2RestartFlow,
            ExhibitionForestRoguelikeFlow,
            ExhibitionClearFlow,

            GoOtherFlowTestFlow,
        }
    }
}