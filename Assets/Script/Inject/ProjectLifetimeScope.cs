using VContainer;
using VContainer.Unity;
using Tarahiro;
using gaw241201.Model.MasterData;
using gaw241201.Presenter;
using gaw241201.View;

namespace gaw241201.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Log.Comment("ProjectLifetimeScope‚Ì“o˜^ŠJŽn");

            //Conversation
            builder.Register<ConversationModel>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationView>(Lifetime.Singleton).AsSelf();
            builder.Register<ConversationMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            //Flow
            builder.Register<FlowHundler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FlowMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FlowProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            //Manager
            builder.Register<AdapterToModel>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GameManager>();
                entryPoints.Add<ConversationPresenter>();
            });
        }
    }
}
