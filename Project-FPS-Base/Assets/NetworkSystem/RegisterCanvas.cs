using Assets.NetworkSystem.Register;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.NetworkSystem
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

        private Login _login;

        private void Awake()
        {
            _login = GetComponent<Login>();
        }

        private void Start()
        {
            _logInButton.onClick.AddListener(LogIn);
            _switchSignInButton.onClick.AddListener(SwitchSignIn);
        }

        private void LogIn()
        {
            _login.LogInUser(_nameInput.text, _password.text);
        }

        private void SwitchSignIn()
        {
            Debug.Log("Switch SignIn");
        }
    }

}

