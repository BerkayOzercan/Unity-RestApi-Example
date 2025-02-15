using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


namespace Assets.NetworkSystem.Register.Scripts
{
    public class Register : MonoBehaviour
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _eMail;
        [SerializeField]
        private string _password;

        private User _newUser;

        void Start()
        {
            RegisterUser();
        }

        private void RegisterUser()
        {
            StartCoroutine(RegisterRequest(_name, _eMail, _password));
        }

        private IEnumerator RegisterRequest(string userName, string eMail, string password)
        {
            _newUser = new User { username = userName, email = eMail, password = password };
            // Convert to JSON here
            string userJson = JsonUtility.ToJson(_newUser);
            Debug.Log(userJson);

            using (UnityWebRequest request = new UnityWebRequest("http://localhost:5251/api/Account/register", "POST"))
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

