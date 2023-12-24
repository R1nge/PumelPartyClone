using System;
using _Assets.Scripts.Services.Lobbies;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.UIs
{
    public class CharacterSlotUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nicknameText;

        [Header("Main menu")]
        [SerializeField] private GameObject mainMenu;

        [SerializeField] private Button ready;
        [SerializeField] private Button customize;
        [SerializeField] private Button leaveSlot;

        [Header("Customization menu")]
        [SerializeField] private GameObject customizationMenu;

        [SerializeField] private Button appearance;
        [SerializeField] private Button back;

        [Header("Appearance menu")]
        [SerializeField] private GameObject appearanceMenu;

        [SerializeField] private GameObject color;
        [SerializeField] private GameObject hat;
        [SerializeField] private GameObject cape;
        [SerializeField] private Button apply;

        [Inject] private Lobby _lobby;

        private void Awake()
        {
            ready.onClick.AddListener(ChangeReadyState);
        }

        private void ChangeReadyState()
        {
            _lobby.ChangeReadyState(NetworkManager.Singleton.LocalClientId, true);
        }

        public void SetNickname(string nickname)
        {
            nicknameText.text = nickname;
        }
    }
}