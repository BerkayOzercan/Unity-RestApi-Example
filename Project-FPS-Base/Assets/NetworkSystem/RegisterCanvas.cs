using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.NetworkSystem.Register.Scripts
{
    public class RegisterCanvas : MonoBehaviour
    {
        [Header("Log In Menu")]
        [SerializeField]
        private TMP_InputField _nameInput;
        [SerializeField]
        private TMP_InputField _password;
        [SerializeField]
        private Button _logInButton;
        [SerializeField]
        private Button _switchSignInButton;
        [SerializeField]
        private GameObject _registerCanvas;

        public Action<string, string> LogInAction;

        private void Start()
        {
            _logInButton.onClick.AddListener(() => LogInAction?.Invoke(_nameInput.text, _password.text));
            _switchSignInButton.onClick.AddListener(SwitchSignIn);
        }

        private void SwitchSignIn()
        {
            Debug.Log("Switch SignIn");
        }
    }

}

