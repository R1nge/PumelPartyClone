using System;
using UnityEngine;

namespace _Assets.Scripts.Services.Skins
{
    public class SkinService : MonoBehaviour
    {
        [SerializeField] private SkinPartSO[] heads;
        [SerializeField] private SkinPartSO[] bodies;
        private SkinData _currentSkin;
        public int CurrentHeadIndex => _currentSkin.HeadIndex;
        public int CurrentBodyIndex => _currentSkin.BodyIndex;
        public event Action<int> OnHeadChanged;
        public event Action<int> OnBodyChanged;

        public void ChangeHead(int index)
        {
            _currentSkin.HeadIndex = index;
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
                HeadIndex = _currentSkin.HeadIndex,
                BodyIndex = _currentSkin.BodyIndex
            };

            return data;
        }

        public struct SkinData
        {
            public int HeadIndex;
            public int BodyIndex;

            public SkinData(int headIndex, int bodyIndex)
            {
                HeadIndex = headIndex;
                BodyIndex = bodyIndex;
            }
        }
    }
}