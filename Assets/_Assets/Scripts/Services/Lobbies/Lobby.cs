using System;
using System.Collections.Generic;
using _Assets.Scripts.Services.Datas;
using UnityEngine;

namespace _Assets.Scripts.Services.Lobbies
{
    public class Lobby
    {
        private readonly Dictionary<ulong, LobbyPlayerData> _players = new();
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

        public void RemovePlayer(ulong connectionId)
        {
            _players.Remove(connectionId);
            OnPlayerDisconnected?.Invoke(connectionId);
        }
    }
}