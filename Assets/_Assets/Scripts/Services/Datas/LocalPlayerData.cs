using System;
using _Assets.Scripts.Services.Skins;

namespace _Assets.Scripts.Services.Datas
{
    [Serializable]
    public struct LocalPlayerData
    {
        public string Nickname;
        public SkinService.SkinData SkinData;

        public LocalPlayerData(SkinService.SkinData skinData, string nickname)
        {
            SkinData = skinData;
            Nickname = nickname;
        }
    }
}