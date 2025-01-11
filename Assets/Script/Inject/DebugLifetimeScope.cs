using VContainer;
using VContainer.Unity;

namespace gaw241201.Inject
{
#if ENABLE_DEBUG
    public class DebugLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GlobalDebugManager>();
                entryPoints.Add<TypingRoguelikeDebugManager>();
            });

        }
    }
#endif
}
