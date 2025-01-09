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

            RegisterManager(builder);
            RegisterFlow(builder);
            RegisterTyping(builder);
            RegisterStarter(builder);
            RegisterFlag(builder);
            RegisterMonitor(builder);
            RegisterEndGame(builder);
            RegisterSave(builder);
            RegisterEyes(builder);
            RegisterEffect(builder);
            RegisterConfiscate(builder);
            RegisterDeleteUi(builder);
            RegisterClickInput(builder);
            RegisterFreeInput(builder);
            RegisterConversation(builder);

            //Tarahiro
            builder.Register<PlatformInfoProvider>(Lifetime.Singleton).AsSelf();

            //RegisterFlagFlow
            builder.Register<RegisterFlagFlowModel>(Lifetime.Singleton).AsSelf();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
#if ENABLE_DEBUG
                entryPoints.Add<DebugManager>();
#endif
            });
        }

        void RegisterManager(IContainerBuilder builder)
        {
            //Manager
            builder.Register<AdapterFactory<HorrorStoryMainLoopStarter, SaveDataManager>>(Lifetime.Singleton).WithParameter<LifetimeScope[]>(FindObjectsOfType<LifetimeScope>).AsImplementedInterfaces();
            builder.Register<FlowProvider>(Lifetime.Singleton).WithParameter<LifetimeScope[]>(FindObjectsOfType<LifetimeScope>).AsImplementedInterfaces();

            builder.RegisterEntryPoint<GameManager>();

        }

        void RegisterFlow(IContainerBuilder builder)
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

        void RegisterTyping(IContainerBuilder builder)
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

        void RegisterStarter(IContainerBuilder builder)
        {

            //starter
            builder.Register<ScreenShotStarter>(Lifetime.Singleton).AsSelf();
            builder.Register<HorrorStoryMainLoopStarter>(Lifetime.Singleton).AsSelf();
#if ENABLE_DEBUG

            builder.Register<TypingTestStarter>(Lifetime.Singleton).AsSelf();
            builder.Register<FreeInputTestStarter>(Lifetime.Singleton).AsSelf();
#endif
        }

        void RegisterFlag(IContainerBuilder builder)
        {

            //Flag
            builder.Register<GlobalFlagContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RegisterFlagOrderProcessor>(Lifetime.Singleton).AsSelf();

        }

        void RegisterMonitor(IContainerBuilder builder)
        {
            //Monitor
            builder.Register<StartMonitorModel>(Lifetime.Singleton).AsSelf();
            builder.Register<MonitorView>(Lifetime.Singleton).AsSelf();
            builder.Register<HaltModel>(Lifetime.Singleton).AsSelf();
            
            builder.RegisterEntryPoint<MonitorPresenter>();

        }

        void RegisterEndGame(IContainerBuilder builder)
        {
            //EndGame
            builder.Register<EndGameModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<EndGameView>().AsSelf();
            
            builder.RegisterEntryPoint<EndGamePresenter>();

        }

        void RegisterSave(IContainerBuilder builder)
        {

            //Save
            builder.Register<SaveDataManager>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<SaveData>(Lifetime.Singleton).AsSelf();


        }

        void RegisterEyes(IContainerBuilder builder)
        {
            //View
            builder.RegisterComponentInHierarchy<EyesView>().AsImplementedInterfaces();
            builder.Register<ImpressionView>(Lifetime.Singleton).AsImplementedInterfaces();

        }

        void RegisterEffect(IContainerBuilder builder)
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

        void RegisterConfiscate(IContainerBuilder builder)
        {
            //Confiscate
            builder.Register<ConfiscateModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<LeftEyeRemovedable>().AsImplementedInterfaces().AsSelf();

            builder.Register<ConfiscateView>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<ConfiscatePresenter>();
        }

        void RegisterDeleteUi(IContainerBuilder builder)
        {

            //DeleteUi
            builder.Register<DeleteUiModel>(Lifetime.Singleton).AsSelf();
            builder.Register<UiDeletableProvider>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        void RegisterClickInput(IContainerBuilder builder)
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

        void RegisterFreeInput(IContainerBuilder builder)
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

        void RegisterConversation(IContainerBuilder builder)
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
    }
}
