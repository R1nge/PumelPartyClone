using _Assets.Scripts.Services.Skins;

namespace _Assets.Scripts.Services.Datas
{
    public class LocalDataLoader
    {
        private readonly SkinService _skinService;

        public LocalDataLoader(SkinService skinService)
        {
            _skinService = skinService;
        }

        public void Load()
        {
            _skinService.LoadSkin();
        }

        public void Save()
        {
        }
    }
}