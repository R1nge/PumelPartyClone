using _Assets.Scripts.Services.Lobbies;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class LobbyLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Lobby>(Lifetime.Singleton);
        }
    }
}