using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.NetworkSystem.Register.Scripts
{
    public class Login : MonoBehaviour
    {
        private string _apiString = "http://localhost:5251/api/Account/login";
        string responseJson;
        public string ErrRespondMessage;

        public Action<bool> IsSuccess;

        public void LogInUser(string name, string password)
        {
            StartCoroutine(LogInRequest(name, password));
        }

        private IEnumerator LogInRequest(string userName, string password)
        {
            LoginUser _currentUser = new LoginUser { username = userName, password = password };
            // Convert to JSON here
            string userJson = JsonUtility.ToJson(_currentUser);
            Debug.Log(userJson);

            using (UnityWebRequest request = new UnityWebRequest(_apiString, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(userJson);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    ErrRespondMessage = "Response: " + request.downloadHandler.text;
                    IsSuccess?.Invoke(true);
                    responseJson = request.downloadHandler.text;
                    // Debug.Log("Success: " + responseJson);

                }
                else
                {
                    ErrRespondMessage = "Response: " + request.downloadHandler.text;
                    IsSuccess?.Invoke(false);
                    // Debug.Log("Error: " + request.responseCode + " - " + request.error);
                    // Debug.Log("Response: " + request.downloadHandler.text);
                }
            }
        }
    }
}