using System;
using _Assets.Scripts.Services.Lobbies;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.UIs
{
    //TODO: make character slot a networked object and place on a separate networked canvas
    public class CharacterSlotUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nicknameText;

        [Header("Server buttons")]
        [SerializeField] private GameObject serverButtons;

        [SerializeField] private Button kick;

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

        private void Start()
        {
            kick.onClick.AddListener(() => Kick(NetworkManager.Singleton.LocalClientId));
        }

        private void ChangeReadyState()
        {
            _lobby.ChangeReadyState(NetworkManager.Singleton.LocalClientId, true);
        }

        private void Kick(ulong clientId)
        {
            _lobby.Kick(clientId);
        }

        public void SetNickname(string nickname)
        {
            nicknameText.text = nickname;
        }

        public void ShowServerButtons()
        {
            serverButtons.SetActive(true);
        }

        public void HideServerButtons()
        {
            serverButtons.SetActive(false);
        }

        public void ShowButtons()
        {
            mainMenu.SetActive(true);
            customizationMenu.SetActive(false);
            appearanceMenu.SetActive(false);
        }

        public void HideButtons()
        {
            mainMenu.SetActive(false);
            customizationMenu.SetActive(false);
            appearanceMenu.SetActive(false);
        }
    }
}