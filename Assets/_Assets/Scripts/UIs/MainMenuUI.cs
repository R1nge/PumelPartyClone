using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button host, join;

        private void Awake()
        {
            host.onClick.AddListener(Host);
            join.onClick.AddListener(Join);
        }

        private void Host() => NetworkManager.singleton.StartHost();

        private void Join() => NetworkManager.singleton.StartClient();
    }
}