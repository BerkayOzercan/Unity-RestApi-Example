using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.NetworkSystem.Register
{
    public class Authentication
    {
        private readonly string _token;
        private readonly MonoBehaviour _monobehavior;
        public Authentication(MonoBehaviour monoBehaviour, string token)
        {
            _token = token;
            _monobehavior = monoBehaviour;
        }

        public void GetRequest(string endpoint, Action<string> callback)
        {
            _monobehavior.StartCoroutine(GetRequestCoroutine(endpoint, callback));
        }

        private IEnumerator GetRequestCoroutine(string endpoint, Action<string> callback)
        {
            string url = $"http://localhost:5251/{endpoint}";
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Content-Type", "application/json");

                if (!string.IsNullOrEmpty(_token))
                {
                    request.SetRequestHeader("Authorization", "Bearer " + _token);
                }

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    callback?.Invoke(request.downloadHandler.text);
                }
                else
                {
                    Debug.LogError($"GET request failed: {request.error}");
                    callback?.Invoke(null);
                }
            }
        }
    }
}


