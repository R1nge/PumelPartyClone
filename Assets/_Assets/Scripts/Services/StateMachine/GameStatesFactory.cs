using _Assets.Scripts.Services.Datas;
using _Assets.Scripts.Services.StateMachine.States;

namespace _Assets.Scripts.Services.StateMachine
{
    public class GameStatesFactory
    {
        private readonly LocalDataLoader _localDataLoader;

        public GameStatesFactory(LocalDataLoader localDataLoader)
        {
            _localDataLoader = localDataLoader;
        }

        public IGameState CreateDataLoadingState(GameStateMachine stateMachine)
        {
            return new DataLoadingState(stateMachine, _localDataLoader);
        }

        public IGameState CreateGameState(GameStateMachine stateMachine)
        {
            return new GameState(stateMachine);
        }
    }
}