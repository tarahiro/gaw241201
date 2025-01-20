using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VitalRouter;

namespace Tarahiro
{
    public class NotifyLanguageCommand : ICommand
    {
        public int LanguageIndex { get; private set; }

        public NotifyLanguageCommand(int languageIndex)
        {
            Log.Comment("ÉRÉ}ÉìÉhê∂ê¨");
            LanguageIndex = languageIndex;
        }
    }
}