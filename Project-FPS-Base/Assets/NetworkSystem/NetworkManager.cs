using System.Collections;
using Assets.GameSystem.Scripts;
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

        protected override void Awake()
        {
            base.Awake();
        }

        public void SetUserData(User user)
        {
            PlayerPrefs.SetString("id", user.id);
            PlayerPrefs.SetString("name", user.username);
        }

        public User GetUserData()
        {
            User newUserData = new User
            {
                id = PlayerPrefs.GetString("id"),
                username = PlayerPrefs.GetString("name")
            };
            return newUserData;
        }

        private void OnPlayerLoggedIn()
        {
            var gameManager = GameManager.Instance;
            gameManager.ChangeState(GameStates.Menu);
            SetLoggedIn(true);
        }

        private void SetLoggedIn(bool value)
        {
            if (value == true)
            {
                PlayerPrefs.SetInt("loggedIn", 1);
            }
            else
            {
                PlayerPrefs.SetInt("loggedIn", 0);
            }
        }

        public bool IsLoggedIn()
        {
            return PlayerPrefs.GetInt("loggedIn") == 1;
        }

        private void OnApplicationQuiting()
        {
            SetLoggedIn(false);
        }

        void OnEnable()
        {
            Register.OnUserRegistered += OnPlayerLoggedIn;
            Login.LoggedIn += OnPlayerLoggedIn;
            Application.quitting += OnApplicationQuiting;
        }

        void OnDisable()
        {
            Register.OnUserRegistered -= OnPlayerLoggedIn;
            Login.LoggedIn -= OnPlayerLoggedIn;
            Application.quitting -= OnApplicationQuiting;

        }
    }
}

