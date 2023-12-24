using System;
using System.Collections.Generic;
using System.Linq;
using _Assets.Scripts.Services.Datas;
using UnityEngine;

namespace _Assets.Scripts.Services.Lobbies
{
    //TODO: make lobby a network object
    //TODO: OR spawn character slots
    public class Lobby
    {
        private readonly Dictionary<ulong, LobbyPlayerData> _players = new(8);
        public IReadOnlyList<LobbyPlayerData> Players => _players.Values.OrderBy(data => data.ConnectionId).ToList();
        public event Action<ulong> OnPlayerConnected;
        public event Action<ulong> OnPlayerDisconnected;

        public void AddPlayer(LobbyPlayerData playerData)
        {
            if (_players.ContainsKey(playerData.ConnectionId))
            {
                Debug.LogError($"ID: {playerData.ConnectionId} already exists");
                return;
            }

            _players.Add(playerData.ConnectionId, playerData);
            OnPlayerConnected?.Invoke(playerData.ConnectionId);
        }

        public void ChangeReadyState(ulong connectionId, bool isReady)
        {
            var player = _players[connectionId];
            player.IsReady = isReady;
            _players[connectionId] = player;
            Debug.LogError("Changed ready state");
        }

        public void RemovePlayer(ulong connectionId)
        {
            _players.Remove(connectionId);
            OnPlayerDisconnected?.Invoke(connectionId);
        }
    }
}