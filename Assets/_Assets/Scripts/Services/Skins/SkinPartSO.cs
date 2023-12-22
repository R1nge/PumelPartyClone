using Unity.Netcode;
using UnityEngine;

namespace _Assets.Scripts.Services.Skins
{
    [CreateAssetMenu(fileName = "Skin Part", menuName = "Configs/Skins/Skin Part")]
    public class SkinPartSO : ScriptableObject
    {
        [SerializeField] private NetworkObject skinPart;
        public NetworkObject SkinPart => skinPart;
    }
}