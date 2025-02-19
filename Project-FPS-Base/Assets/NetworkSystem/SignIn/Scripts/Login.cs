using System;
using System.Collections;
using System.Text;
using Assets.NetworkSystem.Player;
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

        private string _authToken = "";
        private User _newUserData;

        public static Action<string, string> LoggedPlayer;
        public static Action LoggedIn;

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
                    string _responseJson = request.downloadHandler.text;
                    // Try to extract token if the response contains one
                    RegisterResponse registerResponse = JsonUtility.FromJson<RegisterResponse>(_responseJson);

                    if (registerResponse != null)
                    {
                        Debug.Log("Server Response: " + _responseJson); // Debugging output

                        Debug.Log($"User Registered - ID: {registerResponse.userId}, Username: {registerResponse.userName}");

                        _newUserData = new User
                        {
                            id = registerResponse.userId,
                            username = registerResponse.userName ?? "Unknown", // Prevent null values

                        };
                        NetworkManager.Instance.UserData = _newUserData;

                        if (!string.IsNullOrEmpty(registerResponse.token))
                        {
                            _authToken = registerResponse.token;
                            NetworkManager.Instance.UserAuthToken = _authToken;
                        }

                        Respond(true, "Success!");

                        LoggedPlayer?.Invoke(_newUserData.id, _authToken);
                        LoggedIn?.Invoke();
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