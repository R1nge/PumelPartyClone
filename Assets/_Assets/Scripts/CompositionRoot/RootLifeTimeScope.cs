using _Assets.Scripts.Services;
using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.Skins;
using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class RootLifeTimeScope : LifetimeScope
    {
        [SerializeField] private SkinService skinService;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(skinService);
            builder.Register<NicknameService>(Lifetime.Singleton);
            builder.Register<LocalDataLoader>(Lifetime.Singleton);
            builder.Register<GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
        }
    }
}