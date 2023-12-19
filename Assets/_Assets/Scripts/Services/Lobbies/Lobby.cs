using System;
using System.Collections.Generic;
using _Assets.Scripts.Services.Datas;
using UnityEngine;

namespace _Assets.Scripts.Services.Lobbies
{
    public class Lobby
    {
        private readonly Dictionary<int, LobbyPlayerData> _players = new();
        public event Action<int> OnPlayerConnected;
        public event Action<int> OnPlayerDisconnected;

        public void AddPlayer(int connectionId, string nickname, int skinIndex)
        {
            if (_players.ContainsKey(connectionId))
            {
                Debug.LogError($"ID: {connectionId} already exists");
                return;
            }

            _players.Add(connectionId, new LobbyPlayerData(connectionId, nickname, skinIndex));
            OnPlayerConnected?.Invoke(connectionId);
        }

        public void RemovePlayer(int connectionId)
        {
            _players.Remove(connectionId);
            OnPlayerDisconnected?.Invoke(connectionId);
        }
    }
}