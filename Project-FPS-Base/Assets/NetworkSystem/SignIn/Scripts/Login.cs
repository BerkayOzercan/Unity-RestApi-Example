using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    public class Login : MonoBehaviour
    {
        [SerializeField]
        private RespondPopUp _respondPopUp = null;

        private LogInCanvas _logInCanvas = null;

        private string _apiString = "http://localhost:5251/api/Account/login";
        private string _responseJson = "";
        private string _authToken = "";

        ///?/////?/////??///?//
        public static Action<bool> IsLoginAccess;

        private void Awake() { _logInCanvas = GetComponent<LogInCanvas>(); }

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
                    Respond(true, "Success!");
                    _responseJson = request.downloadHandler.text;
                    // Try to extract token if the response contains one
                    RegisterResponse tokenResponse = JsonUtility.FromJson<RegisterResponse>(_responseJson);
                    if (!string.IsNullOrEmpty(tokenResponse.token))
                    {
                        _authToken = tokenResponse.token;
                        NetworkManager.Instance.UserAuthToken = _authToken;
                    }

                }
                else if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Respond(false, "Connection Error");
                }
                else
                {
                    Respond(false, request.downloadHandler.text);
                }
            }
        }

        private void Respond(bool value, string text)
        {
            var respondText = Instantiate(_respondPopUp, transform.parent);
            respondText.SetMessage(value, text);
        }

        private void OnEnable()
        {
            _logInCanvas.LogInAction += LogInUser;
        }

        private void OnDisable()
        {
            _logInCanvas.LogInAction -= LogInUser;
        }
    }
}