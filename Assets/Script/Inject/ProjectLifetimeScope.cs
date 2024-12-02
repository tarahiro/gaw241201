using VContainer;
using VContainer.Unity;
using Tarahiro;
using gaw241201.Model.MasterData;
using gaw241201.Presenter;
using gaw241201.View;
using gaw241201.Model;

namespace gaw241201.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Log.Comment("ProjectLifetimeScope�̓o�^�J�n");

            //Eyes
            builder.RegisterComponentInHierarchy<EyesView>().AsImplementedInterfaces();

            //Confiscate
            builder.Register<ConfiscateModel>(Lifetime.Singleton).AsSelf();
            builder.Register<ConfiscateView>(Lifetime.Singleton).AsSelf();  
            builder.RegisterComponentInHierarchy<LeftEyeRemovedable>().AsImplementedInterfaces().AsSelf() ;

            //Typing
            builder.Register<TypingModel>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<ITypingMaster>>(Lifetime.Singleton).AsSelf();
            builder.Register<TypingViewArgsFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<TypingView>().AsSelf();


            //DeleteUi
            builder.Register<DeleteUiModel>(Lifetime.Singleton).AsSelf();
            builder.Register<UiDeletableProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            // ClickInput
            builder.Register<ClickInputModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<ClickInputView>().AsSelf();
            builder.Register<DeleteClickInputUi>(Lifetime.Singleton).AsSelf();


            //FreeInput
            builder.Register<FreeInputModel>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<FreeInputView>().AsSelf();
            builder.Register<FreeInputValueRegisterer>(Lifetime.Singleton).AsSelf();
            builder.Register<DeleteFreeInputUi>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<FreeInputView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FreeInputArgsFactory>(Lifetime.Singleton).AsSelf();

            //RegisterFlagFlow
            builder.Register<RegisterFlagFlowModel>(Lifetime.Singleton).AsSelf();

            //Conversation
            builder.Register<ConversationModel>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationView>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ModelArgsFactory<IConversationMaster>>(Lifetime.Singleton).AsSelf();
           // builder.Register<ConversationModelArgsFactory>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationViewArgsFactory>(Lifetime.Singleton).AsSelf();
            builder.RegisterComponentInHierarchy<ConversationTextView>().AsSelf();
            builder.Register<MessageKeyHundler>(Lifetime.Singleton).AsSelf();
            builder.Register<MessageKeyReplacerProvider>(Lifetime.Singleton).AsSelf();

            //Flow
            builder.Register<FlowHundler>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<FlowMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FlowProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ApplicationTimeKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<DiffSecondKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<RowKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<DeviceLowerKeyReplacer>(Lifetime.Singleton).AsSelf();
            builder.Register<DeviceKeyReplacer>(Lifetime.Singleton).AsSelf();

            //Flag
            builder.Register<GlobalFlagContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<RegisterFlagOrderProcessor>(Lifetime.Singleton).AsSelf();

            //Manager
            builder.Register<AdapterToModel>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GameManager>();
                entryPoints.Add<ConversationPresenter>();
                entryPoints.Add<FreeInputPresenter>();
                entryPoints.Add<ClickInputPresenter>();
                entryPoints.Add<TypingPresenter>();
                entryPoints.Add<ConfiscatePresenter>();
#if ENABLE_DEBUG
                entryPoints.Add<DebugManager>();
#endif
            });
        }
    }
}
