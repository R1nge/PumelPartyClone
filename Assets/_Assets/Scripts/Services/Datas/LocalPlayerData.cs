using System;

namespace _Assets.Scripts.Services.Datas
{
    [Serializable]
    public struct LocalPlayerData
    {
        public int HeadSkinIndex;
        public int BodySkinIndex;
        public string Nickname;

        public LocalPlayerData(int headSkinIndex, int bodySkinIndex, string nickname)
        {
            HeadSkinIndex = headSkinIndex;
            BodySkinIndex = bodySkinIndex;
            Nickname = nickname;
        }
    }
}