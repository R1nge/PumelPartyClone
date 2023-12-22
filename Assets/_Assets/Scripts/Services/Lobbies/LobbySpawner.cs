using _Assets.Scripts.Services.Datas;
using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Lobbies
{
    public class LobbySpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkObject lobbyPlayerPrefab;
        [Inject] private Lobby _lobby;
        [Inject] private LocalDataLoader _localDataLoader;
        [Inject] private IObjectResolver _objectResolver;

        private void Awake() => NetworkManager.Singleton.OnClientConnectedCallback += ClientConnected;

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                var lobbyPlayerData = new LobbyPlayerData
                {
                    ConnectionId = NetworkManager.Singleton.LocalClientId,
                    Nickname = _localDataLoader.LocalPlayerData.Nickname,
                    SkinIndex = _localDataLoader.LocalPlayerData.SkinIndex
                };

                SpawnLobbyPlayerServerRpc(lobbyPlayerData);
            }
        }

        private void ClientConnected(ulong clientId)
        {
            var rpc = new ClientRpcParams
            {
                Send = new ClientRpcSendParams
                {
                    TargetClientIds = new[] { clientId }
                }
            };

            GetLocalDataClientRpc(rpc);
        }

        [ClientRpc]
        private void GetLocalDataClientRpc(ClientRpcParams clientRpcParams)
        {
            var data = _localDataLoader.LocalPlayerData;

            Debug.LogError(data.Nickname + " " + data.SkinIndex);

            var lobbyPlayerData = new LobbyPlayerData
            {
                ConnectionId = NetworkManager.Singleton.LocalClientId,
                Nickname = data.Nickname,
                SkinIndex = data.SkinIndex
            };

            SpawnLobbyPlayerServerRpc(lobbyPlayerData);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnLobbyPlayerServerRpc(LobbyPlayerData lobbyPlayerData)
        {
            var lobbyPlayer = _objectResolver.Instantiate(lobbyPlayerPrefab);
            lobbyPlayer.SpawnWithOwnership(lobbyPlayerData.ConnectionId);
            _lobby.AddPlayer(lobbyPlayerData);
            Debug.LogError("Spawned player with data: ID: " + lobbyPlayerData.ConnectionId + " Nickname: " +
                           lobbyPlayerData.Nickname + " SkinIndex: " + lobbyPlayerData.SkinIndex);
        }
    }
}