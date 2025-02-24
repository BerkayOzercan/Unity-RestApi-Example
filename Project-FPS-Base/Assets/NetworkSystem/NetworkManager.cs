using System.Collections;
using Assets.GameSystem.Scripts;
using Assets.NetworkSystem.Player;
using Assets.NetworkSystem.SignIn.Scripts;
using UnityEngine;

namespace Assets.NetworkSystem
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        public void SetUserData(User user)
        {
            PlayerPrefs.SetString("userId", user.id);
            PlayerPrefs.SetString("userName", user.username);
        }

        public User GetUserData()
        {
            User newUserData = new User
            {
                id = PlayerPrefs.GetString("userId"),
                username = PlayerPrefs.GetString("userName")
            };
            return newUserData;
        }

        public void SetUserAuthToken(string token)
        {
            PlayerPrefs.SetString("userAuthToken", token);
        }

        public string GetUserAuthToken()
        {
            return PlayerPrefs.GetString("userAuthToken");
        }

        public void SetPlayerData(PlayerData playerData)
        {
            PlayerPrefs.SetInt("playerId", playerData.id);
            PlayerPrefs.SetString("playerName", playerData.userName);
            PlayerPrefs.SetInt("playerRank", playerData.rank);
            PlayerPrefs.SetFloat("playerScore", (float)playerData.score);
            PlayerPrefs.SetString("playerUserId", playerData.userId);
        }

        public PlayerData GetPLayerData()
        {
            PlayerData newPlayerData = new PlayerData
            {
                id = PlayerPrefs.GetInt("playerId"),
                userName = PlayerPrefs.GetString("playerName"),
                rank = PlayerPrefs.GetInt("playerRank"),
                score = (decimal)PlayerPrefs.GetFloat("playerScore"),
                userId = PlayerPrefs.GetString("playerUserId")
            };

            return newPlayerData;
        }

        private void OnPlayerLoggedIn()
        {
            var gameManager = GameManager.Instance;
            gameManager.ChangeState(GameStates.Start);
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

