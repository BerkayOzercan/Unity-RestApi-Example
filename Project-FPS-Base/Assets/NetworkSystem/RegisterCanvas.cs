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

        [Header("Respond PopUp")]
        [SerializeField]
        private TMP_Text _respondText;
        [SerializeField]
        private Button _boardCloseButton;
        [SerializeField]
        private GameObject _PopUpBoard;

        private Login _login;

        private void Awake()
        {
            _login = GetComponent<Login>();
        }

        private void Start()
        {
            _logInButton.onClick.AddListener(LogIn);
            _switchSignInButton.onClick.AddListener(SwitchSignIn);
            _boardCloseButton.onClick.AddListener(() => CloseMenu(_PopUpBoard, false));
        }

        public void ShowResultMessage(bool isSuccess)
        {
            _PopUpBoard.SetActive(true);

            if (isSuccess)
            {
                _respondText.text = _login.ErrRespondMessage;
                _PopUpBoard.SetActive(false);
                _registerCanvas.SetActive(false);
            }
            else
            {
                _respondText.text = _login.ErrRespondMessage;
            }
        }

        private void LogIn()
        {
            _login.LogInUser(_nameInput.text, _password.text);
        }

        private void SwitchSignIn()
        {
            Debug.Log("Switch SignIn");
        }

        private void CloseMenu(GameObject obj, bool value)
        {
            obj.SetActive(value);
        }

        private void OnEnable()
        {
            _login.IsSuccess += ShowResultMessage;
        }

        private void OnDisable()
        {
            _login.IsSuccess -= ShowResultMessage;
        }
    }

}

