using _Assets.Scripts.Services.Datas;

namespace _Assets.Scripts.Services.StateMachine.States
{
    public class DataLoadingState : IGameState
    {
        private readonly LocalDataLoader _localDataLoader;
        private readonly GameStateMachine _stateMachine;

        public DataLoadingState(GameStateMachine stateMachine, LocalDataLoader localDataLoader)
        {
            _stateMachine = stateMachine;
            _localDataLoader = localDataLoader;
        }

        public void Enter()
        {
            _localDataLoader.Load();
        }

        public void Exit()
        {
        }
    }
}