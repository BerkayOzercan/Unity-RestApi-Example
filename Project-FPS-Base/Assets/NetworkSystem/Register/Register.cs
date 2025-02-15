using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


namespace Assets.NetworkSystem.Register.Scripts
{
    public class Register : MonoBehaviour
    {
        [SerializeField]
        private string _name = string.Empty;
        [SerializeField]
        private string _eMail = string.Empty;
        [SerializeField]
        private string _password = string.Empty;

        void Start()
        {
            //RegisterUser(_name, _eMail, _password);
            StartCoroutine(RegisterRequest());
        }


        // public void RegisterUser(string username, string eMail, string password)
        // {
        //     StartCoroutine(RegisterRequest(username, eMail, password));
        // }

        private IEnumerator RegisterRequest()
        {
            //Need to convert Unity Json System
            // this one is work
            string jsonData = "{\"username\":\"newuser4444\",\"email\":\"newuser4444@example.com\",\"password\":\"P@ssword_4444\"}";
            print(jsonData);

            using (UnityWebRequest request = new UnityWebRequest("http://localhost:5251/api/Account/register", "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
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

