using VContainer;
using VContainer.Unity;
using Tarahiro;
using gaw241201.Model.MasterData;

namespace gaw241201.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Log.Comment("ProjectLifetimeScope�̓o�^�J�n");

            //Conversation
            builder.Register<ConversationModel>(Lifetime.Singleton).AsSelf();

            //Flow
            builder.Register<FlowHundler>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FlowMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<FlowProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            //Manager
            builder.Register<AdapterToModel>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GameManager>();
            });
        }
    }
}
