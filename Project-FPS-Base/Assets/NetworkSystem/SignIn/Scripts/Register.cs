using System;
using System.Collections;
using System.Text;
using Assets.NetworkSystem.Player;
using UnityEngine;
using UnityEngine.Networking;
namespace Assets.NetworkSystem.SignIn.Scripts
{
    public class Register : MonoBehaviour
    {
        private string _apiString = "http://localhost:5251/api/Account/register";

        [SerializeField]
        private RespondPopUp _respondPopUp = null;

        private RegisterCanvas _registerCanvas = null;
        private User _newUser;
        private PlayerData _newPlayerData;
        private string _authToken = "";

        public static Action OnUserRegistered;

        private void Awake()
        {
            _registerCanvas = GetComponent<RegisterCanvas>();
        }

        private void RegisterUser(string name, string eMail, string password)
        {
            StartCoroutine(RegisterRequest(name, eMail, password));
        }

        private IEnumerator RegisterRequest(string userName, string eMail, string password)
        {
            _newUser = new User { username = userName, email = eMail, password = password };
            // Convert to JSON here
            string userJson = JsonUtility.ToJson(_newUser);

            using (UnityWebRequest request = new UnityWebRequest(_apiString, "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(userJson);
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string responseJson = request.downloadHandler.text;
                    Debug.Log("Server Response: " + responseJson); // Debugging output

                    RegisterResponse registerResponse = JsonUtility.FromJson<RegisterResponse>(responseJson);

                    if (registerResponse != null)
                    {
                        Debug.Log($"User Registered - ID: {registerResponse.id}, Username: {registerResponse.username}");

                        _newPlayerData = new PlayerData
                        {
                            id = 0,
                            username = registerResponse.username ?? "Unknown", // Prevent null values
                            rank = 0,
                            score = 0,
                            userid = registerResponse.id
                        };
                        NetworkManager.Instance.PlayerData = _newPlayerData;

                        if (!string.IsNullOrEmpty(registerResponse.token))
                        {
                            _authToken = registerResponse.token;
                            NetworkManager.Instance.UserAuthToken = _authToken;
                            Debug.Log("Token Received: " + _authToken);
                        }

                        Respond(true, "Success!");
                        OnUserRegistered?.Invoke();
                    }
                }
                else if (request.result == UnityWebRequest.Result.ConnectionError)
                {
                    Respond(false, "Connection Error!");
                }
                else { Respond(false, request.downloadHandler.text); }
            }
        }

        private void Respond(bool value, string text)
        {
            var respondText = Instantiate(_respondPopUp, transform.parent);
            respondText.SetMessage(value, text);
        }

        private void OnEnable()
        {
            _registerCanvas.RegisterAction += RegisterUser;
        }

        private void OnDisable()
        {
            _registerCanvas.RegisterAction -= RegisterUser;
        }
    }
}

