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

            //skill
            builder.Register<SkillAchieveModel>(Lifetime.Singleton).AsSelf();
            builder.Register<SkillAchieveArgsDataFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<SkillAchieveView>().AsSelf();

            //act
            builder.Register<ActStartModel>(Lifetime.Singleton).AsSelf();
            builder.Register<StageMasterListGetter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<IStageMasterRegisteredRestrictedCharList>>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<ActView>().AsSelf();
            builder.Register<ActPresenter>(Lifetime.Singleton).AsSelf();
            builder.Register<ActViewArgsListFactory>(Lifetime.Singleton).AsSelf();

            //flag
            builder.Register<AchievableMasterFlagContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RestrictedCharContainer>(Lifetime.Singleton).AsImplementedInterfaces();

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
            builder.Register<TypingRoguelikeModel>(Lifetime.Singleton).AsSelf().As<IRequiredScoreGeneratable>();
            builder.Register<TypingRoguelikeSingleSequenceStarter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeGroupMasterGetter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingInitializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<ITypingRoguelikeSingleSequenceMaster>>(Lifetime.Singleton).AsSelf();
            builder.Register<IndexUpdater>(Lifetime.Singleton).AsSelf();
            builder.Register<SelectionDataContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeSingleSequenceMasterFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RestrictionGenerator>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<WaveClearModel>(Lifetime.Singleton).AsSelf();

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
            builder.RegisterComponentInHierarchy<RequiredPointView>().AsSelf();

            //stageBg
            builder.RegisterComponentInHierarchy<StageBgView>().AsSelf();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<TypingRoguelikePresetner>();
                entryPoints.Add<ActPresenter>();
                entryPoints.Add<SkillPresenter>();
#if ENABLE_DEBUG
                entryPoints.Add<TypingRoguelikeDebugManager>();
#endif
            });
        }
    }
}
