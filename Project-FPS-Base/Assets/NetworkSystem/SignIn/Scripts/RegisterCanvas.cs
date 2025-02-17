using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    public class RegisterCanvas : MonoBehaviour
    {
        [Header("Register Menu")]
        [SerializeField]
        private TMP_InputField _registerNameInput;
        [SerializeField]
        private TMP_InputField _registerEMailInput;
        [SerializeField]
        private TMP_InputField _registerPasswordInput;
        [SerializeField]
        private Button _registerButton;
        [SerializeField]
        private Button _backButton;

        public Action<string, string, string> RegisterAction;

        private void Awake()
        {
            _registerButton.onClick.AddListener(() =>
                RegisterAction?.Invoke(_registerNameInput.text, _registerEMailInput.text, _registerPasswordInput.text));

            _backButton.onClick.AddListener(ChangeState);
        }

        private void ChangeState()
        {
            SignInManager.Instance.ChangeState(SignInStates.Login);
        }
    }
}


