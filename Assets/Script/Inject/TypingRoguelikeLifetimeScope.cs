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

            //Flag
            builder.Register<AchievableMasterFlagContainer<ILeetMaster>>(Lifetime.Singleton).AsImplementedInterfaces();

            //Leet
            builder.Register<LeetMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<AvaliableLeetMasterDataProvider>(Lifetime.Singleton).AsSelf();

            //TypingRoguelike
            //model
            builder.Register<TypingRoguelikeModel>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingRoguelikeSingleSequenceStarter<ITypingMaster>>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<MergedGroupMasterGetter<ITypingMaster, ITypingRoguelikeMaster>>(Lifetime.Singleton).AsImplementedInterfaces();

            //view
            builder.Register<RoguelikeCorrectInputHundler>(Lifetime.Singleton).AsSelf().As<IPointableView>();
            builder.Register<TypingRoguelikeView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LeetInputJudger>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<TimerView>().AsImplementedInterfaces();
            builder.Register<TypingSentenceController>(Lifetime.Singleton).AsSelf();

            //presenter
            builder.Register<LeetDataListFactory>(Lifetime.Singleton).AsSelf();

            //pointŠÖ˜A
            builder.Register<PointModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<PointView>().AsSelf();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<TypingRoguelikePresetner>();
            });
        }
    }
}
