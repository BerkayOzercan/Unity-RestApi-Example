using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.NetworkSystem
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        public bool IsLogInSuccessful { get; set; }

        private string apiUrl = "https://your-api.com/register";

        public IEnumerator LogIn(string usernameInput, string passwordInput)
        {
            var requestData = new
            {
                username = usernameInput,
                password = passwordInput
            };

            string jsonData = JsonUtility.ToJson(requestData);
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

            UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                IsLogInSuccessful = true;
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                IsLogInSuccessful = false;
                Debug.LogError("Error: " + request.error);
            }
        }
    }
}

