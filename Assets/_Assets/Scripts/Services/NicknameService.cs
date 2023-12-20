using System;

namespace _Assets.Scripts.Services
{
    public class NicknameService
    {
        private readonly Random _random = new();
        private string _nickname;

        public string LoadNickname()
        {
            var number = _random.Next(0, 1000);
            return $"Player + {number}";
        }
    }
}