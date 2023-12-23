using System;
using _Assets.Scripts.Services.Skins;
using Unity.Netcode;

namespace _Assets.Scripts.Services.Datas
{
    [Serializable]
    public struct LobbyPlayerData : INetworkSerializable, IEquatable<LobbyPlayerData>
    {
        public ulong ConnectionId;
        public string Nickname;
        public SkinService.SkinData SkinData;

        public LobbyPlayerData(ulong connectionId, string nickname, SkinService.SkinData skinData)
        {
            ConnectionId = connectionId;
            Nickname = nickname;
            SkinData = skinData;
        }

        public bool Equals(LobbyPlayerData other)
        {
            return ConnectionId == other.ConnectionId && Nickname == other.Nickname && SkinData.Equals(other.SkinData);
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ConnectionId);
            serializer.SerializeValue(ref Nickname);
            serializer.SerializeValue(ref SkinData.HatIndex);
            serializer.SerializeValue(ref SkinData.BodyIndex);
        }

        public override bool Equals(object obj)
        {
            return obj is LobbyPlayerData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ConnectionId, Nickname, SkinData);
        }
    }
}