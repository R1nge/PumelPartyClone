using _Assets.Scripts.Services.Datas;
using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Lobbies
{
    public class LobbySpawner : NetworkBehaviour
    {
        [SerializeField] private Transform parent;
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
                    SkinData = _localDataLoader.LocalPlayerData.SkinData
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

            Debug.LogError(data.Nickname + " Head: " + data.SkinData.HeadIndex + " Body: " + data.SkinData.BodyIndex);

            var lobbyPlayerData = new LobbyPlayerData
            {
                ConnectionId = NetworkManager.Singleton.LocalClientId,
                Nickname = data.Nickname,
                SkinData = data.SkinData
            };

            SpawnLobbyPlayerServerRpc(lobbyPlayerData);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnLobbyPlayerServerRpc(LobbyPlayerData lobbyPlayerData)
        {
            _lobby.AddPlayer(lobbyPlayerData);
            //var lobbyPlayer = _objectResolver.Instantiate(lobbyPlayerPrefab, parent);
            //lobbyPlayer.SpawnWithOwnership(lobbyPlayerData.ConnectionId);
            Debug.LogError("Spawned player with data: ID: " + lobbyPlayerData.ConnectionId + " Nickname: " +
                           lobbyPlayerData.Nickname + "Head: " + lobbyPlayerData.SkinData.HeadIndex + " Body: " +
                           lobbyPlayerData.SkinData.BodyIndex);
        }
    }
}