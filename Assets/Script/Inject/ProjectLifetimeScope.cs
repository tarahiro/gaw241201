using VContainer;
using VContainer.Unity;
using Tarahiro;
using gaw241201.Model.MasterData;
using gaw241201.Presenter;
using gaw241201.View;
using gaw241201.Model;
using UnityEngine;
using Tarahiro.Ui;
using Tarahiro.MasterData;
using System;
using System.Linq;
using MessagePipe;
using MessagePipe.VContainer;
using UnityEditor;

namespace gaw241201.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            Log.Comment("ProjectLifetimeScopeの登録開始");

            ConfigureGlobalFactory(builder);
            ConfigureManager(builder);
            ConfigureFlow(builder);
            ConfigureTyping(builder);
            ConfigureStarter(builder);
            ConfigureFlag(builder);
            ConfigureMonitor(builder);
            ConfigureEndGame(builder);
            ConfigureSave(builder);
            ConfigureEyes(builder);
            ConfigureEffect(builder);
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
            ConfigureLanguage(builder);
            ConfigureSetting(builder);
            ConfigureRenderer(builder);
        }

        void ConfigureGlobalFactory(IContainerBuilder builder)
        {
            builder.Register<ConversationViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<ConversationTextViewProvider>().AsSelf();
        }

        void ConfigureManager(IContainerBuilder builder)
        {
            //Manager
            builder.Register<AdapterFactory<SimpleLoopStarter, SaveDataManager>>(Lifetime.Singleton).WithParameter<LifetimeScope[]>(FindObjectsOfType<LifetimeScope>).AsImplementedInterfaces();
            builder.Register<FlowProvider>(Lifetime.Singleton).WithParameter<LifetimeScope[]>(FindObjectsOfType<LifetimeScope>).AsImplementedInterfaces();

            builder.Register<SimpleLoopStarter>(Lifetime.Singleton).AsSelf().WithParameter(FlowMasterConst.FlowMasterLabel.CompanyFlow);

            builder.RegisterEntryPoint<GameManager>();


            //Debug
            builder.Register<AnimationPublisherFake>(Lifetime.Singleton).AsSelf();

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
            builder.Register<LoopInitializer>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        void ConfigureTyping(IContainerBuilder builder)
        {
            //model
            builder.Register<TypingMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<ITypingMaster>>(Lifetime.Singleton).AsSelf();
            builder.Register<QuestionDisplayTextModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SimpleGroupMasterGetter<ITypingMaster>>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SingleTextSequenceEnterer<ITypingMaster>>(Lifetime.Singleton).AsImplementedInterfaces();

            //view
            builder.Register<TypingView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<TypingTextView>().AsSelf();
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
            builder.Register<StartMonitorFlowModel>(Lifetime.Singleton).AsSelf();
            builder.Register<MonitorView>(Lifetime.Singleton).AsSelf();
            builder.Register<CmdHaltModel>(Lifetime.Singleton).AsSelf();
            builder.Register<StartMonitorModel>(Lifetime.Singleton).AsSelf();

            builder.Register<MonitorViewItemProvider>(Lifetime.Singleton).AsSelf();
            builder.Register<CmdMonitorView>(Lifetime.Singleton).AsSelf();
            builder.Register<SettingMonitorView>(Lifetime.Singleton).AsSelf();
            
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
            builder.RegisterComponentInHierarchy<MainEyesView>().AsSelf().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<SettingEyesView>().AsSelf().As<ISettingOrnament>();
            builder.Register<ImpressionView>(Lifetime.Singleton).AsSelf();
            builder.Register<GazeMessagePublisher>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FacialMessagePublisher>(Lifetime.Singleton).AsSelf();
            builder.Register<ResetGazeMessagePublisher>(Lifetime.Singleton).AsSelf();

            /*
            var options = builder.RegisterMessagePipe();
            builder.RegisterMessageBroker<GazeConst.GazingKey, Vector2>(options);
            */

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
            builder.RegisterComponentInHierarchy<LeftEyeRemovedable>().AsImplementedInterfaces().AsSelf();
            builder.RegisterComponentInHierarchy<EyesPositionChangable>().AsSelf();
            builder.RegisterComponentInHierarchy<ConversationTextPositionChangable>().AsSelf();

            //Confiscate
            builder.RegisterEntryPoint<EffectPresenter>();

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
            builder.Register<ConversationMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<IConversationMaster>>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationViewArgsFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<ConversationTextView>().AsSelf();
            builder.Register<MessageKeyHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<MessageKeyReplacerProvider>(Lifetime.Singleton).AsSelf();
            builder.Register<FakeSkillChoicesDecider>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<ConversationModelFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationModelProvider>(Lifetime.Singleton).AsSelf();


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
            builder.RegisterComponentInHierarchy<SkillEnterView>().AsSelf();
            builder.Register<SkillIndexInputView>(Lifetime.Singleton).AsSelf();
            builder.Register<SkillMenuModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<SkillMenuView>().AsSelf();

            builder.RegisterEntryPoint<SkillPresenter>();
        }

        void ConfigureAct(IContainerBuilder builder)
        {

            //act
            builder.Register<ActStartModel>(Lifetime.Singleton).AsSelf();
            builder.Register<StageMasterListGetter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<IStageMasterRegisteredRestrictedCharList>>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<ActUiView>().AsSelf();
            builder.Register<ActPresenter>(Lifetime.Singleton).AsSelf();
            builder.Register<ActUiViewArgsListFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterEntryPoint<ActPresenter>();

            builder.RegisterComponentInHierarchy<StageBgView>().AsSelf().AsImplementedInterfaces();
            builder.Register<ActMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();


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
            builder.Register<TypingRoguelikeModel>(Lifetime.Singleton).AsSelf().As<ITimerEndableModel>();
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
            builder.Register<EnterKeyHundler>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SimpleCorrectInputHundler>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<RoguelikeRestrictInputHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<RoguelikeCorrectInputHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingRoguelikeConditionProvider>(Lifetime.Singleton).AsSelf();
            builder.Register<RomanInputProcesser>(Lifetime.Singleton).AsSelf();

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

            //timer関連
            builder.Register<TimerModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TimerStarter>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.Register<RequiredScoreGenerator>(Lifetime.Singleton).AsImplementedInterfaces();

        }

        [SerializeField] TranslationTextView[] textViewList;

        void ConfigureLanguage(IContainerBuilder builder)
        {
            builder.Register<LanguageModel>(Lifetime.Singleton).AsSelf();
            builder.Register<LanguagePublisher>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<LanguageMessageMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterEntryPoint<LanguageInitializer>(Lifetime.Singleton).AsSelf().WithParameter<LanguageConst.AvailableLanguage>(LanguageConst.AvailableLanguage.Japanese);
            builder.RegisterEntryPoint<EmbeddedTextPresenter>(Lifetime.Singleton).AsSelf();

            var options = builder.RegisterMessagePipe(/* configure option */);
            builder.RegisterMessageBroker<int>(options);

            builder.Register<EmbeddedTextViewManager>(Lifetime.Singleton).AsSelf();
        }

        void ConfigureSetting(IContainerBuilder builder)
        {
            builder.Register<SettingRootHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<SettingStarter>(Lifetime.Singleton).AsSelf();
            builder.Register<SettingExiter>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<SettingRootView>().AsSelf();
            builder.Register<SettingInputView>(Lifetime.Singleton).AsSelf();

            builder.Register<SettingUiModel>(Lifetime.Singleton).AsSelf();
            builder.Register<SettingTabListFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<ProfileMenuModel>(Lifetime.Singleton).AsSelf();
            builder.Register<AdvancedTabModel>(Lifetime.Singleton).AsSelf();

            builder.RegisterComponentInHierarchy<SettingTabManager>().AsSelf();

            builder.RegisterEntryPoint<SettingPresenter>();
        }

        void ConfigureRenderer(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<RendererHundler>().AsSelf();
        }

    }


}
