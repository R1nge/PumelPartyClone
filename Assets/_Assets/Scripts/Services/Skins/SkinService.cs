using System;
using UnityEngine;

namespace _Assets.Scripts.Services.Skins
{
    public class SkinService
    {
        private int _currentSkinIndex;
        public int CurrentSkinIndex => _currentSkinIndex;
        public event Action<int> OnSkinChanged;

        public void ChangeSkin(int index)
        {
            _currentSkinIndex = index;
            OnSkinChanged?.Invoke(index);
        }

        public int LoadSkin()
        {
            Debug.Log("Loaded skins data");
            return _currentSkinIndex;
        }
    }
}