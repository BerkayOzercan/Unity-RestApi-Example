using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    public class LogInCanvas : MonoBehaviour
    {
        [Header("Log In Menu")]
        [SerializeField]
        private TMP_InputField _logInNameInput;
        [SerializeField]
        private TMP_InputField _logInPassword;
        [SerializeField]
        private Button _logInButton;
        [SerializeField]
        private Button _registerButton;

        public Action<string, string> LogInAction;

        private void Awake()
        {
            _logInButton.onClick.AddListener(() =>
                LogInAction?.Invoke(_logInNameInput.text, _logInPassword.text));

            _registerButton.onClick.AddListener(ChangeState);
        }

        private void ChangeState()
        {
            SignInManager.Instance.ChangeState(SignInStates.Register);
        }
    }
}


