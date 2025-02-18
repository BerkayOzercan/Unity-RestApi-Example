using System.Collections;
using System.Text;
using Assets.NetworkSystem.SignIn.Scripts;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.NetworkSystem.Player
{
    public class PlayerDataController : MonoBehaviour
    {
        private string apiUrl = "http://localhost:5251/api/player";

        private string AccessToken()
        {
            var token = NetworkManager.Instance.UserAuthToken;

            if (token == string.Empty)
            {
                Debug.LogError("TokenEmpty");
                return null;
            }
            return token;
        }

        private User UserData()
        {
            return NetworkManager.Instance.UserData;
        }

        public void CreatePlayer()
        {
            PlayerData newPlayer = new PlayerData
            {
                username = UserData().username,
                rank = 0,
                score = 0,
                userId = UserData().id
            };

            StartCoroutine(CreatePlayerCoroutine(newPlayer));
        }

        public void GetPlayerById(string userId)
        {
            StartCoroutine(GetPlayerByIdCoroutine(userId));
        }

        public void UpdatePlayer(PlayerData player)
        {
            StartCoroutine(UpdatePlayerCoroutine(player));
        }

        private IEnumerator CreatePlayerCoroutine(PlayerData playerData)
        {
            UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
            request.SetRequestHeader("Authorization", "Bearer " + AccessToken());
            string json = JsonUtility.ToJson(playerData);


            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            Debug.Log(request.result);

            if (request.result == UnityWebRequest.Result.Success)
                Debug.Log("Player created: " + request.downloadHandler.text);
            else
                Debug.LogError("Error: " + request.error);
        }

        private IEnumerator GetPlayerByIdCoroutine(string userId)
        {
            UnityWebRequest request = UnityWebRequest.Get(apiUrl + "/" + userId);

            request.SetRequestHeader("Authorization", "Bearer " + AccessToken());

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                PlayerData player = JsonUtility.FromJson<PlayerData>(request.downloadHandler.text);
                Debug.Log($"Player: {player.username}, Score: {player.score}");
            }
            else
            {
                Debug.LogError("Error: " + request.error);
            }
        }

        private IEnumerator UpdatePlayerCoroutine(PlayerData player)
        {
            string json = JsonUtility.ToJson(player);
            UnityWebRequest request = new UnityWebRequest(apiUrl + "/" + player.id, "PUT");

            request.SetRequestHeader("Authorization", "Bearer " + AccessToken());

            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
                Debug.Log("Player updated: " + request.downloadHandler.text);
            else
                Debug.LogError("Error: " + request.error);
        }

        void OnEnable()
        {
            Register.OnUserRegistered += CreatePlayer;
        }

        void OnDisable()
        {
            Register.OnUserRegistered -= CreatePlayer;
        }
    }
}


