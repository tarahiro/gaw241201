using VContainer;
using VContainer.Unity;
using Tarahiro;
using gaw241201.Model.MasterData;
using gaw241201.Presenter;
using gaw241201.View;
using gaw241201.Model;
using UnityEngine;

namespace gaw241201.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            Log.Comment("ProjectLifetimeScope‚Ì“o˜^ŠJŽn");


            //Tarahiro
            builder.Register<PlatformInfoProvider>(Lifetime.Singleton).AsSelf();

            //Manager
            builder.Register<AdapterFactory<HorrorStoryMainLoopStarter,SaveDataManager>>(Lifetime.Singleton).WithParameter<LifetimeScope[]>(FindObjectsOfType<LifetimeScope>).AsImplementedInterfaces();
        }
    }
}
