using TMPro;
using UnityEngine;

namespace _Assets.Scripts.Services.Lobbies
{
    public class LobbyCharacterTopBar : MonoBehaviour
    {
        //Top bar in the lobby, contains player's name and device icon (keyboard or controller)
        //It can be a MB, which is predefined in the lobby scene, when player connects a client rpc will update the text and icon
        [SerializeField] private TextMeshProUGUI playerName;

        public void SetNickname(string nickname) => playerName.text = nickname;
    }
}