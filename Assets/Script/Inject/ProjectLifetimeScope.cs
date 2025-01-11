using VContainer;
using VContainer.Unity;
using Tarahiro;
using gaw241201.Model.MasterData;
using gaw241201.Presenter;
using gaw241201.View;
using gaw241201.Model;
using UnityEngine;
using Tarahiro.MasterData;

namespace gaw241201.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            Log.Comment("ProjectLifetimeScopeの登録開始");

            CofigureManager(builder);
            ConfigureFlow(builder);
            ConfigureTyping(builder);
            ConfigureStarter(builder);
            ConfigureFlag(builder);
            ConfigureMonitor(builder);
            ConfigureEndGame(builder);
            ConfigureSave(builder);
            ConfigureEyes(builder);
            ConfigureEffect(builder);
            ConfigureConfiscate(builder);
            ConfigureDeleteUi(builder);
            ConfigureClickInput(builder);
            ConfigureFreeInput(builder);
            ConfigureConversation(builder);

            //Tarahiro
            builder.Register<PlatformInfoProvider>(Lifetime.Singleton).AsSelf();

            //RegisterFlagFlow
            builder.Register<RegisterFlagFlowModel>(Lifetime.Singleton).AsSelf();

            //以下、ローグライクから移植してきたもの

            //starter
            builder.Register<TypingRoguelikeMainLoopStarter>(Lifetime.Singleton).AsSelf();


            ConfigureSkill(builder);
            ConfigureAct(builder);
            ConfigureRestriction(builder);
            ConfigureWord(builder);
            ConfigureLeet(builder);
            ConfigureTypingRoguelike(builder);
            ConfigureStageBg(builder);
        }

        void CofigureManager(IContainerBuilder builder)
        {
            //Manager
            builder.Register<AdapterFactory<HorrorStoryMainLoopStarter, SaveDataManager>>(Lifetime.Singleton).WithParameter<LifetimeScope[]>(FindObjectsOfType<LifetimeScope>).AsImplementedInterfaces();
            builder.Register<FlowProvider>(Lifetime.Singleton).WithParameter<LifetimeScope[]>(FindObjectsOfType<LifetimeScope>).AsImplementedInterfaces();

            builder.RegisterEntryPoint<GameManager>();

        }

        void ConfigureFlow(IContainerBuilder builder)
        {
            //model
            builder.Register<FlowHundler>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<FlowMasterDataDictionaryProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ApplicationTimeKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<DiffSecondKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<RowKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<DeviceLowerKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<DeviceKeyReplacer>(Lifetime.Singleton).AsSelf();
        }

        void ConfigureTyping(IContainerBuilder builder)
        {
            //model
            builder.Register<TypingModel>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<ITypingMaster>>(Lifetime.Singleton).AsSelf();
            builder.Register<RomanKeyInputJudger>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<QuestionDisplayTextModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SimpleGroupMasterGetter<ITypingMaster>>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SingleTextSequenceEnterer<ITypingMaster>>(Lifetime.Singleton).AsImplementedInterfaces();

            //view
            builder.Register<TypingViewArgsFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<TypingTextView>().AsSelf();

           builder.RegisterEntryPoint<TypingPresenter>();
        }

        void ConfigureStarter(IContainerBuilder builder)
        {

            //starter
            builder.Register<ScreenShotStarter>(Lifetime.Singleton).AsSelf();
            builder.Register<HorrorStoryMainLoopStarter>(Lifetime.Singleton).AsSelf();
#if ENABLE_DEBUG

            builder.Register<TypingTestStarter>(Lifetime.Singleton).AsSelf();
            builder.Register<FreeInputTestStarter>(Lifetime.Singleton).AsSelf();
#endif
        }

        void ConfigureFlag(IContainerBuilder builder)
        {

            //Flag
            builder.Register<GlobalFlagContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RegisterFlagOrderProcessor>(Lifetime.Singleton).AsSelf();
            builder.Register<AchievableMasterFlagContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RestrictedCharContainer>(Lifetime.Singleton).AsImplementedInterfaces();

        }

        void ConfigureMonitor(IContainerBuilder builder)
        {
            //Monitor
            builder.Register<StartMonitorModel>(Lifetime.Singleton).AsSelf();
            builder.Register<MonitorView>(Lifetime.Singleton).AsSelf();
            builder.Register<HaltModel>(Lifetime.Singleton).AsSelf();
            
            builder.RegisterEntryPoint<MonitorPresenter>();

        }

        void ConfigureEndGame(IContainerBuilder builder)
        {
            //EndGame
            builder.Register<EndGameModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<EndGameView>().AsSelf();
            
            builder.RegisterEntryPoint<EndGamePresenter>();

        }

        void ConfigureSave(IContainerBuilder builder)
        {

            //Save
            builder.Register<SaveDataManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SaveData>(Lifetime.Singleton).AsSelf();


        }

        void ConfigureEyes(IContainerBuilder builder)
        {
            //View
            builder.RegisterComponentInHierarchy<EyesView>().AsImplementedInterfaces();
            builder.Register<ImpressionView>(Lifetime.Singleton).AsImplementedInterfaces();

        }

        void ConfigureEffect(IContainerBuilder builder)
        {
            //Effect
            builder.Register<EnterEffectModel>(Lifetime.Singleton).AsSelf();
            builder.Register<EndEffectModel>(Lifetime.Singleton).AsSelf();
            builder.Register<EffectArgsFactory>(Lifetime.Singleton).AsSelf();

            //View
            builder.RegisterComponentInHierarchy<EffectView>().AsImplementedInterfaces();
            builder.Register<EffectViewItemFactory>(Lifetime.Singleton).AsSelf();

            //Confiscate
            builder.RegisterEntryPoint<EffectPresenter>();

        }

        void ConfigureConfiscate(IContainerBuilder builder)
        {
            //Confiscate
            builder.Register<ConfiscateModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<LeftEyeRemovedable>().AsImplementedInterfaces().AsSelf();

            builder.Register<ConfiscateView>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<ConfiscatePresenter>();
        }

        void ConfigureDeleteUi(IContainerBuilder builder)
        {

            //DeleteUi
            builder.Register<DeleteUiModel>(Lifetime.Singleton).AsSelf();
            builder.Register<UiDeletableProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        void ConfigureClickInput(IContainerBuilder builder)
        {
            //Model
            builder.Register<ClickInputModel>(Lifetime.Singleton).AsSelf();
            builder.Register<DeleteClickInputUi>(Lifetime.Singleton).AsSelf();
            builder.Register<ClickInputProccessorProvider>(Lifetime.Singleton).AsSelf();
            builder.Register<DoubleAffirmation>(Lifetime.Singleton).AsSelf();
            builder.Register<Skippable>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            //View
            builder.RegisterComponentInHierarchy<ClickInputView>().AsSelf();

            builder.RegisterEntryPoint<ClickInputPresenter>();
        }

        void ConfigureFreeInput(IContainerBuilder builder)
        {

            //FreeInput
            builder.Register<FreeInputModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<FreeInputView>().AsSelf();
            builder.Register<FreeInputValueRegisterer>(Lifetime.Singleton).AsSelf();
            builder.Register<DeleteFreeInputUi>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<FreeInputView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FlowViewArgsFactory>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<FreeInputPresenter>();
        }

        void ConfigureConversation(IContainerBuilder builder)
        {
            //Conversation
            builder.Register<ConversationModel>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationView>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<IConversationMaster>>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationViewArgsFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<ConversationTextView>().AsSelf();
            builder.Register<MessageKeyHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<MessageKeyReplacerProvider>(Lifetime.Singleton).AsSelf();

            builder.Register<DeviceModelKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<DeviceModelLowerKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<DeviceTypeFake>(Lifetime.Singleton).AsSelf();
            builder.Register<GraphicsName>(Lifetime.Singleton).AsSelf();
            builder.Register<GraphicsType>(Lifetime.Singleton).AsSelf();
            builder.Register<GraphicsVendor>(Lifetime.Singleton).AsSelf();
            builder.Register<GraphicsVersion>(Lifetime.Singleton).AsSelf();
            builder.Register<SimpleGroupMasterGetter<IConversationMaster>>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SingleTextSequenceEnterer<IConversationMaster>>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<ConversationPresenter>();
        }

        void ConfigureSkill(IContainerBuilder builder)
        {

            //skill
            builder.Register<SkillAchieveModel>(Lifetime.Singleton).AsSelf();
            builder.Register<SkillAchieveArgsDataFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<SkillAchieveView>().AsSelf();

            builder.RegisterEntryPoint<SkillPresenter>();
        }

        void ConfigureAct(IContainerBuilder builder)
        {

            //act
            builder.Register<ActStartModel>(Lifetime.Singleton).AsSelf();
            builder.Register<StageMasterListGetter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<IStageMasterRegisteredRestrictedCharList>>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<ActView>().AsSelf();
            builder.Register<ActPresenter>(Lifetime.Singleton).AsSelf();
            builder.Register<ActViewArgsListFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<ActPresenter>();


        }
        void ConfigureRestriction(IContainerBuilder builder)
        {

            //Restriction
            builder.Register<RestrictionMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();

        }

        void ConfigureWord(IContainerBuilder builder)
        {

            //Word
            builder.Register<WordMasterDataProvider>(Lifetime.Singleton).
                As<IWordMasterDataProvider>().
                As<IMasterDataProvider<IMasterDataRecord<IWordMaster>>>();
            builder.Register<AvailableWordDataProvider>(Lifetime.Singleton).
                As<AvaliableMasterDataProvider<IMasterDataRecord<IWordMaster>>>().
                As<IAvailableMasterDataProvider<IMasterDataRecord<IWordMaster>>>();
        }

        void ConfigureLeet(IContainerBuilder builder)
        {


            //Leet
            builder.Register<LeetMasterDataProvider>(Lifetime.Singleton).
                As<ILeetMasterDataProvider>().
                As<IMasterDataProvider<IMasterDataRecord<ILeetMaster>>>();
            builder.Register<AvailableLeetDataProvider>(Lifetime.Singleton).
                As<AvaliableMasterDataProvider<IMasterDataRecord<ILeetMaster>>>().
                As<IAvailableMasterDataProvider<IMasterDataRecord<ILeetMaster>>>();

        }

        void ConfigureTypingRoguelike(IContainerBuilder builder)
        {

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
            builder.Register<EnterKeyHundler>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SimpleCorrectInputHundler>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<RoguelikeRestrictInputHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<RoguelikeCorrectInputHundler>(Lifetime.Singleton).AsSelf();

            //view
            builder.Register<TypingRoguelikeView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<TimerView>().AsImplementedInterfaces();
            builder.Register<KeyInputProcesser>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<TypingTextView>().AsImplementedInterfaces();

            //presenter
            builder.Register<TypingRoguelikeViewArgsFactory>(Lifetime.Singleton).AsSelf();

            //point関連
            builder.Register<PointModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<PointView>().AsSelf();
            builder.RegisterComponentInHierarchy<RequiredPointView>().AsSelf();

            builder.RegisterEntryPoint<TypingRoguelikePresetner>();


        }

        void ConfigureStageBg(IContainerBuilder builder)
        {
            //stageBg
            builder.RegisterComponentInHierarchy<StageBgView>().AsSelf();

        }

    }


}
