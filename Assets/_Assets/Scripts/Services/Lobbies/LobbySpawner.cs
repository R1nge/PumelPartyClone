using _Assets.Scripts.Services.Datas;
using Mirror;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services.Lobbies
{
    public class LobbySpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject lobbyPlayerPrefab;
        [Inject] private Lobby _lobby;
        [Inject] private LocalDataLoader _localDataLoader;
        [Inject] private IObjectResolver _objectResolver;

        public override void OnStartServer()
        {
            base.OnStartServer();
            NetworkServer.OnConnectedEvent += OnClientConnect;
            NetworkServer.RegisterHandler<PlayerConnectedMessage>(OnCreateCharacter);
        }

        private void OnClientConnect(NetworkConnectionToClient client)
        {
            PlayerConnectedMessage playerConnectedMessage = new PlayerConnectedMessage
            {
                connectionId = client.connectionId,
                nickname = $"Joe {Random.Range(0, 1000)}",
                skinIndex = 0
            };

            NetworkClient.Send(playerConnectedMessage);
        }

        private void OnCreateCharacter(NetworkConnectionToClient conn, PlayerConnectedMessage playerConnectedMessage)
        {
            var player = _objectResolver.Instantiate(lobbyPlayerPrefab);
            NetworkServer.Spawn(player, conn);
            AddPlayerData(playerConnectedMessage.connectionId, playerConnectedMessage.nickname,
                playerConnectedMessage.skinIndex);
        }

        private void AddPlayerData(int connectionId, string nickname, int skinIndex)
        {
            var lobbyData = new LobbyPlayerData
            {
                ConnectionId = connectionId,
                Nickname = nickname,
                SkinIndex = skinIndex
            };

            _lobby.AddPlayer(lobbyData.ConnectionId, lobbyData.Nickname, lobbyData.SkinIndex);
            Debug.LogError(
                $"Client connected: {lobbyData.ConnectionId}, nickname: {lobbyData.Nickname}, skin: {lobbyData.SkinIndex}");
        }

        public struct PlayerConnectedMessage : NetworkMessage
        {
            public int connectionId;
            public string nickname;
            public int skinIndex;

            public PlayerConnectedMessage(int connectionId, string nickname, int skinIndex)
            {
                this.connectionId = connectionId;
                this.nickname = nickname;
                this.skinIndex = skinIndex;
            }
        }
    }
}