using VContainer;
using VContainer.Unity;
using Tarahiro.TInput;
using Tarahiro;

namespace gaw241201.Inject
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PureSingletonInput>(Lifetime.Singleton).AsSelf();
            builder.Register<PureSingletonKey>(Lifetime.Singleton).AsSelf();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                /*
                //Touch
                entryPoints.Add<TTouch>();
                entryPoints.Add<TFlick>();
                entryPoints.Add<TInput>();
                entryPoints.Add<Tkey>();
                entryPoints.Add<TCanvas>();
                */

                entryPoints.Add<PureSingletonInputUpdater>();
                entryPoints.Add<PureSingletonKeyUpdater>();

#if ENABLE_VIRTUAL_CURSOR
                entryPoints.Add<TVIrtualCursor>();
#endif
            });
        }
    }
}
