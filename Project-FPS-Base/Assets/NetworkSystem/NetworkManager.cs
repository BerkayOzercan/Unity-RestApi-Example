using System.Collections;
using Assets.NetworkSystem.Player;
using Assets.NetworkSystem.SignIn.Scripts;
using UnityEngine;

namespace Assets.NetworkSystem
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        [SerializeField]
        private PlayerData _playerData;
        [SerializeField]
        private User _userData;
        private string _userAuthToken;
        public string UserAuthToken { get { return _userAuthToken; } set { _userAuthToken = value; } }
        public PlayerData PlayerData { get { return _playerData; } set { _playerData = value; } }
        public User UserData { get { return _userData; } set { _userData = value; } }
    }
}

