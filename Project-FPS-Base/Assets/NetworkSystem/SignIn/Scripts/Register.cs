using System.Collections;
using System.Text;
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
        private string _authToken = "";

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
                    }
                    Respond(true, "Success!");
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

