using Mirror.Core;
using UnityEngine;

namespace _Assets.Scripts.Services.Lobbies
{
    public class LobbyMono : MonoBehaviour
    {
        private Lobby _lobby;

        private void Awake()
        {
            NetworkServer.OnConnectedEvent += OnClientConnected;
            NetworkServer.OnDisconnectedEvent += OnClientDisconnected;
        }

        private void OnClientConnected(NetworkConnectionToClient client)
        {
            _lobby.AddPlayer(client.connectionId, "Player", 0);
        }

        private void OnClientDisconnected(NetworkConnectionToClient client)
        {
            _lobby.RemovePlayer(client.connectionId);
        }
    }
}