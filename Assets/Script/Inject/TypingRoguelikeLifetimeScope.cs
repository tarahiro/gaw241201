using VContainer;
using VContainer.Unity;
using Tarahiro;
using gaw241201.Model.MasterData;
using gaw241201.Presenter;
using gaw241201.View;
using gaw241201.Model;

namespace gaw241201.Inject
{
    public class TypingRoguelikeLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            //starter
            builder.Register<TypingRoguelikeMainLoopStarter>(Lifetime.Singleton).AsSelf();

            //TypingRoguelike
            builder.Register<TypingRoguelikeModel>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingRoguelikeMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MergedGroupMasterGetter<ITypingMaster, ITypingRoguelikeMaster>>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeSingleSequenceStarter<ITypingMaster>>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<TimerView>().AsImplementedInterfaces();
            builder.Register<TypingRoguelikeView>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<TypingRoguelikePresetner>();
            });
        }
    }
}
