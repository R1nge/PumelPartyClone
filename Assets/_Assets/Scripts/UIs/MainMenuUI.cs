using Mirror.Core;
using UnityEngine;
using UnityEngine.UI;

namespace _Assets.Scripts.UIs
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