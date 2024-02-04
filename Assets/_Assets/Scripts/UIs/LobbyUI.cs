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
        [SerializeField] private GridLayoutGroup characterGrid;
        [SerializeField] private CharacterSlotUI[] characterSlots;
        [Inject] private Lobby _lobby;

        private void Awake()
        {
            _lobby.OnPlayerConnected += UpdateTopBar;
            _lobby.OnPlayerDisconnected += UpdateTopBar;

            _lobby.OnPlayerConnected += UpdateCharacterGrid;
            _lobby.OnPlayerDisconnected += UpdateCharacterGrid;

            NetworkManager.Singleton.OnClientDisconnectCallback += DisconnectServerRpc;
            disconnect.onClick.AddListener(Disconnect);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _lobby.OnPlayerConnected -= UpdateTopBar;
            _lobby.OnPlayerDisconnected -= UpdateTopBar;
            disconnect.onClick.RemoveAllListeners();

            if (NetworkManager.Singleton)
            {
                NetworkManager.Singleton.OnClientDisconnectCallback -= DisconnectServerRpc;
            }
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
                    lobbyCharactersTopBar[i].gameObject.SetActive(true);
                    UpdateTopBarClientRpc(nickname, i);
                }
                else
                {
                    lobbyCharactersTopBar[i].SetNickname(string.Empty);
                    lobbyCharactersTopBar[i].gameObject.SetActive(false);
                    UpdateTopBarClientRpc(string.Empty, i);
                }
            }
        }

        [ClientRpc]
        private void UpdateTopBarClientRpc(string nickname, int index)
        {
            lobbyCharactersTopBar[index].SetNickname(nickname);
            lobbyCharactersTopBar[index].gameObject.SetActive(nickname != string.Empty);
        }

        private void UpdateCharacterGrid(ulong clientId)
        {
            ChangeCellSize(_lobby.Players.Count);

            for (int i = 0; i < characterSlots.Length; i++)
            {
                if (i < _lobby.Players.Count)
                {
                    characterSlots[i].SetNickname(_lobby.Players[i].Nickname);
                    characterSlots[i].gameObject.SetActive(true);
                    UpdateCharacterGridClientRpc(_lobby.Players.Count, i, _lobby.Players[i].Nickname,
                        _lobby.Players[i].ConnectionId);
                }
                else
                {
                    characterSlots[i].gameObject.SetActive(false);
                    UpdateCharacterGridClientRpc(_lobby.Players.Count, i, string.Empty, _lobby.Players[i].ConnectionId);
                }
            }
        }

        [ClientRpc]
        private void UpdateCharacterGridClientRpc(int playerCount, int index, string nickname,
            ulong senderConnectionId)
        {
            ChangeCellSize(playerCount);

            characterSlots[index].SetNickname(nickname);
            characterSlots[index].gameObject.SetActive(nickname != string.Empty);

            if (NetworkManager.LocalClientId == senderConnectionId)
            {
                if (IsServer || IsHost)
                {
                    if (index == 0)
                    {
                        characterSlots[index].ShowButtons();
                    }
                    else
                    {
                        characterSlots[index].ShowServerButtons();
                    }
                }
                else
                {
                    characterSlots[index].ShowButtons();
                }
            }
            else
            {
                if (IsServer || IsHost)
                {
                    characterSlots[index].ShowServerButtons();
                }
                else
                {
                    characterSlots[index].HideButtons();
                }
            }
        }

        private void ChangeCellSize(int playerCount)
        {
            if (playerCount > 4)
            {
                characterGrid.cellSize = new Vector2(characterGrid.cellSize.x, 290);
            }
            else
            {
                characterGrid.cellSize = new Vector2(characterGrid.cellSize.x, 580);
            }
        }

        public override void OnNetworkSpawn() => start.gameObject.SetActive(IsServer);
    }
}