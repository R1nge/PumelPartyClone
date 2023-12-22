using System;
using Unity.Netcode;

namespace _Assets.Scripts.Services.Datas
{
    [Serializable]
    public struct LobbyPlayerData : INetworkSerializable, IEquatable<LobbyPlayerData>
    {
        public ulong ConnectionId;
        public string Nickname;
        public int HeadSkinIndex;
        public int BodySkinIndex;

        public LobbyPlayerData(ulong connectionId, string nickname, int headSkinIndex, int bodySkinIndex)
        {
            ConnectionId = connectionId;
            Nickname = nickname;
            HeadSkinIndex = headSkinIndex;
            BodySkinIndex = bodySkinIndex;
        }

        public bool Equals(LobbyPlayerData other)
        {
            return ConnectionId == other.ConnectionId && Nickname == other.Nickname &&
                   HeadSkinIndex == other.HeadSkinIndex;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ConnectionId);
            serializer.SerializeValue(ref Nickname);
            serializer.SerializeValue(ref HeadSkinIndex);
            serializer.SerializeValue(ref BodySkinIndex);
        }

        public override bool Equals(object obj)
        {
            return obj is LobbyPlayerData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ConnectionId, Nickname, HeadSkinIndex, BodySkinIndex);
        }
    }
}