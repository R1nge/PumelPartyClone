using System;

namespace _Assets.Scripts.Services.Datas
{
    [Serializable]
    public struct LobbyPlayerData
    {
        public int ConnectionId;
        public string Nickname;
        public int SkinIndex;

        public LobbyPlayerData(int connectionId, string nickname, int skinIndex)
        {
            ConnectionId = connectionId;
            Nickname = nickname;
            SkinIndex = skinIndex;
        }
    }
}