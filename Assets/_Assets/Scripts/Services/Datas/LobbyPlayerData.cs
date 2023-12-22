using System;
using Unity.Netcode;

namespace _Assets.Scripts.Services.Datas
{
    [Serializable]
    public struct LobbyPlayerData : INetworkSerializable, IEquatable<LobbyPlayerData>
    {
        public ulong ConnectionId;
        public string Nickname;
        public int SkinIndex;

        public LobbyPlayerData(ulong connectionId, string nickname, int skinIndex)
        {
            ConnectionId = connectionId;
            Nickname = nickname;
            SkinIndex = skinIndex;
        }

        public bool Equals(LobbyPlayerData other)
        {
            return ConnectionId == other.ConnectionId && Nickname == other.Nickname && SkinIndex == other.SkinIndex;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ConnectionId);
            serializer.SerializeValue(ref Nickname);
            serializer.SerializeValue(ref SkinIndex);
        }

        public override bool Equals(object obj)
        {
            return obj is LobbyPlayerData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ConnectionId, Nickname, SkinIndex);
        }
    }
}