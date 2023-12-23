using System;
using UnityEngine;

namespace _Assets.Scripts.Services.Skins
{
    public class SkinService : MonoBehaviour
    {
        [SerializeField] private SkinPartSO[] heads;
        [SerializeField] private SkinPartSO[] bodies;
        private SkinData _currentSkin;
        public int CurrentHatIndex => _currentSkin.HatIndex;
        public int CurrentBodyIndex => _currentSkin.BodyIndex;
        public event Action<int> OnHeadChanged;
        public event Action<int> OnBodyChanged;

        public void ChangeHat(int index)
        {
            _currentSkin.HatIndex = index;
            OnHeadChanged?.Invoke(index);
        }

        public void ChangeBody(int index)
        {
            _currentSkin.BodyIndex = index;
            OnBodyChanged?.Invoke(index);
        }

        public SkinData LoadSkin()
        {
            Debug.Log("Loaded skins data");

            var data = new SkinData
            {
                HatIndex = _currentSkin.HatIndex,
                BodyIndex = _currentSkin.BodyIndex
            };

            return data;
        }

        public struct SkinData
        {
            public int HatIndex;
            public int BodyIndex;

            public SkinData(int hatIndex, int bodyIndex)
            {
                HatIndex = hatIndex;
                BodyIndex = bodyIndex;
            }
        }
    }
}