using System;
using System.Collections;
using System.Text;
using Assets.NetworkSystem.Register.Scripts;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.NetworkSystem.Register
{
    public class Login : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _password;

        private User _newUser;

        void Start()
        {
            LogInUser();
        }

        private void LogInUser()
        {
            StartCoroutine(LogInRequest(_name, _password));
        }

        private IEnumerator LogInRequest(string userName, string password)
        {
            _newUser = new User { username = userName, password = password };
            // Convert to JSON here
            string userJson = JsonUtility.ToJson(_newUser);
            Debug.Log(userJson);

            using (UnityWebRequest request = new UnityWebRequest("http://localhost:5251/api/Account/login", "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(userJson);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string responseJson = request.downloadHandler.text;
                    Debug.Log("Success: " + responseJson);
                }
                else
                {
                    Debug.LogError("Error: " + request.responseCode + " - " + request.error);
                    Debug.LogError("Response: " + request.downloadHandler.text);
                }
            }
        }
    }
}