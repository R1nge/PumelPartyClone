using _Assets.Scripts.Services.StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace _Assets.Scripts
{
    public class Boot : MonoBehaviour
    {
        [Inject] private GameStateMachine _gameStateMachine;

        private void Start()
        {
            _gameStateMachine.SwitchState(GameStateType.DataLoading);
            SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        }
    }
}