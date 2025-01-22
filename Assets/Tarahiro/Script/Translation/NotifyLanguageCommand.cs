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

        ILanguageMessageMasterDataProvider _masterDataProvider;

        public int LanguageIndex { get; private set; }




        public NotifyLanguageCommand(int languageIndex, ILanguageMessageMasterDataProvider masterDataProvider)
        {
            Log.Comment("ÉRÉ}ÉìÉhê∂ê¨");
            LanguageIndex = languageIndex;
            _masterDataProvider = masterDataProvider;
        }


        public bool TryGetMessage(string Id, out string message) {

            var master = _masterDataProvider.TryGetFromId(Id);
            if (master != null)
            {
                message = master.GetMaster().Message.GetTranslatedText(LanguageIndex);
                return true;
            }
            else
            {
                message = "";
                return false;
            }
        
        }
    }
}