using System.Collections;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;


namespace Assets.NetworkSystem.Register.Scripts
{
    public class Register : MonoBehaviour
    {
        private string _apiString = "http://localhost:5251/api/Account/register";

        [SerializeField]
        private string _name;
        [SerializeField]
        private string _eMail;
        [SerializeField]
        private string _password;

        private User _newUser;
        private string _authToken = "";

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

                    // Try to extract token if the response contains one
                    TokenRespons tokenResponse = JsonUtility.FromJson<TokenRespons>(responseJson);
                    if (!string.IsNullOrEmpty(tokenResponse.token))
                    {
                        _authToken = tokenResponse.token;
                        NetworkManager.Instance.UserAccessToken = _authToken;
                        Debug.Log("Token: " + _authToken);

                        // PlayerPrefs.SetString("authToken", authToken); // Save token locally
                        // PlayerPrefs.Save();
                    }

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

