using System;

namespace _Assets.Scripts.Services.Datas
{
    [Serializable]
    public struct LocalPlayerData
    {
        public int SkinIndex;
        public string Nickname;

        public LocalPlayerData(int skinIndex, string nickname)
        {
            SkinIndex = skinIndex;
            Nickname = nickname;
        }
    }
}