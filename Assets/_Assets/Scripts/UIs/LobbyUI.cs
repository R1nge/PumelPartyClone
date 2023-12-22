using System;
using _Assets.Scripts.Services.Lobbies;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.UIs
{
    public class LobbyUI : NetworkBehaviour
    {
        [SerializeField] private Button start, disconnect;
        [SerializeField] private LobbyCharacterTopBar[] lobbyCharactersTopBar;
        [Inject] private Lobby _lobby;

        private void Awake()
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += DisconnectServerRpc;

            _lobby.OnPlayerConnected += UpdateTopBar;
            _lobby.OnPlayerDisconnected += UpdateTopBar;

            disconnect.onClick.AddListener(Disconnect);
        }

        private void Disconnect()
        {
            DisconnectServerRpc(NetworkManager.Singleton.LocalClientId);
            NetworkManager.Singleton.Shutdown();
            SceneManager.LoadSceneAsync("MainMenu");
        }

        [ServerRpc(RequireOwnership = false)]
        private void DisconnectServerRpc(ulong clientId)
        {
            Debug.LogError("Removed player from lobby: " + clientId);
            _lobby.RemovePlayer(clientId);
        }

        private void UpdateTopBar(ulong clientId)
        {
            for (int i = 0; i < lobbyCharactersTopBar.Length; i++)
            {
                if (i < _lobby.Players.Count)
                {
                    var nickname = _lobby.Players[i].Nickname;
                    lobbyCharactersTopBar[i].SetNickname(nickname);
                    UpdateTopBarClientRpc(nickname, i);
                }
                else
                {
                    lobbyCharactersTopBar[i].SetNickname(string.Empty);
                    UpdateTopBarClientRpc(string.Empty, i);
                }
            }
        }

        [ClientRpc]
        private void UpdateTopBarClientRpc(string nickname, int index)
        {
            lobbyCharactersTopBar[index].SetNickname(nickname);
        }

        public override void OnNetworkSpawn() => start.gameObject.SetActive(IsServer);
    }
}