using Assets.NetworkSystem.Player;
using Assets.NetworkSystem.SignIn.Scripts;

namespace Assets.NetworkSystem
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private string _userAuthToken;
        private PlayerData _playerData;
        private User _userData;
        public string UserAuthToken { get { return _userAuthToken; } set { _userAuthToken = value; } }
        public PlayerData PlayerData { get { return _playerData; } set { _playerData = value; } }
        public User UserData { get { return _userData; } set { _userData = value; } }
    }
}

