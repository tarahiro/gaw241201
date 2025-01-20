using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tarahiro;
using UniRx;
using UnityEngine;
using VContainer;
using VitalRouter;

namespace Tarahiro
{
    public class LanguageCommandProcessor
    {
        [Inject]
        private ICommandPublisher _publisher;

        public void On(int languageIndex)
        {
            Log.Comment("����R�}���h���s");
            _publisher.PublishAsync(new NotifyLanguageCommand(languageIndex));
        }
    }
}