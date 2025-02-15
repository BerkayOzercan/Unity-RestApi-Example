using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.NetworkSystem
{
    public class NetworkManager : Singleton<NetworkManager>
    {
        private string _serverUrl = "http://localhost:5251/api";

        public string GetServerUrl()
        {
            return _serverUrl;
        }
    }
}

