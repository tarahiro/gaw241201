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
           
            /*
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
#if ENABLE_DEBUG
                entryPoints.Add<TypingRoguelikeDebugManager>();
#endif
            });
            */
        }
        void RegisterSkill(IContainerBuilder builder)
        {

            //skill
            builder.Register<SkillAchieveModel>(Lifetime.Singleton).AsSelf();
            builder.Register<SkillAchieveArgsDataFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<SkillEnterView>().AsSelf();

            builder.RegisterEntryPoint<SkillPresenter>();
        }

        void RegisterAct(IContainerBuilder builder)
        {

            //act
            builder.Register<ActStartModel>(Lifetime.Singleton).AsSelf();
            builder.Register<StageMasterListGetter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<IStageMasterRegisteredRestrictedCharList>>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<ActUiView>().AsSelf();
            builder.Register<ActPresenter>(Lifetime.Singleton).AsSelf();
            builder.Register<ActUiViewArgsListFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<ActPresenter>();


        }

        void RegisterFlag(IContainerBuilder builder)
        {
            //flag
            builder.Register<AchievableMasterFlagContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RestrictedCharContainer>(Lifetime.Singleton).AsImplementedInterfaces();

        }
        
        void RegisterRestriction(IContainerBuilder builder)
        {

            //Restriction
            builder.Register<RestrictionMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();

        }

        void RegisterWord(IContainerBuilder builder)
        {

            //Word
            builder.Register<WordMasterDataProvider>(Lifetime.Singleton).
                As<IWordMasterDataProvider>().
                As<IMasterDataProvider<IMasterDataRecord<IWordMaster>>>();
            builder.Register<AvailableWordDataProvider>(Lifetime.Singleton).
                As<AvaliableMasterDataProvider<IMasterDataRecord<IWordMaster>>>().
                As<IAvailableMasterDataProvider<IMasterDataRecord<IWordMaster>>>();
        }

        void RegisterLeet(IContainerBuilder builder)
        {


            //Leet
            builder.Register<LeetMasterDataProvider>(Lifetime.Singleton).
                As<ILeetMasterDataProvider>().
                As<IMasterDataProvider<IMasterDataRecord<ILeetMaster>>>();
            builder.Register<AvailableLeetDataProvider>(Lifetime.Singleton).
                As<AvaliableMasterDataProvider<IMasterDataRecord<ILeetMaster>>>().
                As<IAvailableMasterDataProvider<IMasterDataRecord<ILeetMaster>>>();

        }

        void RegisterTypingRoguelike(IContainerBuilder builder)
        {

            //TypingRoguelike
            //model
            builder.Register<TypingRoguelikeModel>(Lifetime.Singleton).AsSelf().As<IRequiredScoreGeneratable>();
            builder.Register<TypingRoguelikeSingleSequenceStarter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingRoguelikeGroupMasterGetter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TypingStarter>(Lifetime.Singleton).AsImplementedInterfaces();
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

            builder.RegisterEntryPoint<TypingRoguelikePresetner > ();


        }

        void RegisterStageBg(IContainerBuilder builder)
        {
            //stageBg
            builder.RegisterComponentInHierarchy<StageBgView>().AsSelf();

        }
    }
}
