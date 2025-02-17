using Assets.NetworkSystem.Player;

namespace Assets.NetworkSystem
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private string _userAuthToken;
        private PlayerData _playerData;
        public string UserAuthToken { get { return _userAuthToken; } set { _userAuthToken = value; } }
        public PlayerData PlayerData { get { return _playerData; } set { _playerData = value; } }
    }
}

