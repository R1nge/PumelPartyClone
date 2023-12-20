using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Skins;
using _Assets.Scripts.Services.StateMachine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class RootLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<NicknameService>(Lifetime.Singleton);
            builder.Register<SkinService>(Lifetime.Singleton);
            builder.Register<LocalDataLoader>(Lifetime.Singleton);
            builder.Register<GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
        }
    }
}