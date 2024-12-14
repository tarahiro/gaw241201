using VContainer;
using VContainer.Unity;
using Tarahiro;
using gaw241201.Model.MasterData;
using gaw241201.Presenter;
using gaw241201.View;
using gaw241201.Model;
using Tarahiro.MasterData;

namespace gaw241201.Inject
{
    public class TypingRoguelikeLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            //starter
            builder.Register<TypingRoguelikeMainLoopStarter>(Lifetime.Singleton).AsSelf();

            //Flag
            builder.Register<AchievableMasterFlagContainer>(Lifetime.Singleton).AsImplementedInterfaces();

            //Restriction
            builder.Register<RestrictionMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            //Word
            builder.Register<WordMasterDataProvider>(Lifetime.Singleton).
                As<IWordMasterDataProvider>().
                As<IMasterDataProvider<IMasterDataRecord<IWordMaster>>>();
            builder.Register<AvailableWordDataProvider>(Lifetime.Singleton).
                As<AvaliableMasterDataProvider<IMasterDataRecord<IWordMaster>>>().
                As<IAvailableMasterDataProvider<IMasterDataRecord<IWordMaster>>>();

            //Leet
            builder.Register<LeetMasterDataProvider>(Lifetime.Singleton).
                As<ILeetMasterDataProvider>().
                As<IMasterDataProvider<IMasterDataRecord<ILeetMaster>>>();
            builder.Register<AvailableLeetDataProvider>(Lifetime.Singleton).
                As<AvaliableMasterDataProvider<IMasterDataRecord<ILeetMaster>>>().
                As<IAvailableMasterDataProvider<IMasterDataRecord<ILeetMaster>>>();

            //TypingRoguelike
            //model
            builder.Register<TypingRoguelikeModel>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingRoguelikeSingleSequenceStarter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeSingleSequenceMasterGetter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingInitializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<ITypingRoguelikeSingleSequenceMaster>>(Lifetime.Singleton).AsSelf();

            //view
            builder.Register<RoguelikeRestrictInputHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<RoguelikeCorrectInputHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingRoguelikeView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<TimerView>().AsImplementedInterfaces();
            builder.Register<EnterKeyHundler>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<KeyInputProcesser>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<TypingTextView>().AsImplementedInterfaces();

            //presenter
            builder.Register<TypingRoguelikeViewArgsFactory>(Lifetime.Singleton).AsSelf();

            //pointŠÖ˜A
            builder.Register<PointModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PointView>().AsSelf();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<TypingRoguelikePresetner>();
            });
        }
    }
}
