using TMPro;
using UnityEngine;

namespace Assets.NetworkSystem.Register.Scripts
{
    public class RegisterCanvas : MonoBehaviour
    {
        [Header("Log In Menu")]
        [SerializeField]
        private TMP_InputField _nameInput = null;
        [SerializeField]
        private TMP_InputField _password = null;

        public void LogIn()
        {
            Debug.Log("Name: " + _nameInput.text);
            Debug.Log("Password: " + _password.text);
        }
    }

}

