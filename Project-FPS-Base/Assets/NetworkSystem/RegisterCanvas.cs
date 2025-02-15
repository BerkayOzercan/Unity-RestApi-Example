using TMPro;
using UnityEngine;

namespace Assets.NetworkSystem
{
    public class RegisterCanvas : MonoBehaviour
    {
        [Header("Log In Menu")]
        [SerializeField]
        private TMP_InputField _nameInput = null;
        [SerializeField]
        private TMP_InputField _password = null;

        private NetworkManager _networkManager = null;

        void Awake()
        {
            _networkManager = NetworkManager.Instance;
        }

        //Set to login button
        public void LogIn()
        {
        }
    }

}

