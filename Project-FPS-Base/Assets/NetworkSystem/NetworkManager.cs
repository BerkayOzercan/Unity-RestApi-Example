namespace Assets.NetworkSystem
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private string _userAccessToken;
        public string UserAccessToken { get { return _userAccessToken; } set { _userAccessToken = value; } }
    }
}

