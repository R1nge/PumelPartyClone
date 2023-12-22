using System;
using UnityEngine;

namespace _Assets.Scripts.Services.Skins
{
    public class SkinService : MonoBehaviour
    {
        [SerializeField] private SkinPartSO[] heads;
        [SerializeField] private SkinPartSO[] bodies;
        private int _currentBodyIndex;
        private int _currentHeadIndex;
        public int CurrentHeadIndex => _currentHeadIndex;
        public int CurrentBodyIndex => _currentBodyIndex;
        public event Action<int> OnHeadChanged;
        public event Action<int> OnBodyChanged;

        public void ChangeHead(int index)
        {
            _currentHeadIndex = index;
            OnHeadChanged?.Invoke(index);
        }

        public void ChangeBody(int index)
        {
            _currentHeadIndex = index;
            OnBodyChanged?.Invoke(index);
        }

        public (int, int) LoadSkin()
        {
            Debug.Log("Loaded skins data");
            return (_currentHeadIndex, _currentBodyIndex);
        }
    }
}