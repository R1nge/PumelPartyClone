using _Assets.Scripts.Services.Skins;

namespace _Assets.Scripts.Services.Datas
{
    public class LocalDataLoader
    {
        private readonly NicknameService _nicknameService;
        private readonly SkinService _skinService;

        private LocalDataLoader(SkinService skinService, NicknameService nicknameService)
        {
            _skinService = skinService;
            _nicknameService = nicknameService;
        }

        public LocalPlayerData LocalPlayerData { get; private set; }

        public void Load()
        {
            var skinData = _skinService.LoadSkin();

            LocalPlayerData = new LocalPlayerData
            {
                SkinData = skinData,
                Nickname = _nicknameService.LoadNickname()
            };
        }

        public void Save()
        {
        }
    }
}