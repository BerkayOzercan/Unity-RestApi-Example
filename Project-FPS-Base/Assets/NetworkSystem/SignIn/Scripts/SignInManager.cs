using Assets.MenuSystem.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    public class SignInManager : Singleton<SignInManager>
    {
        [SerializeField]
        private Login _login = null;
        [SerializeField]
        private Register _register = null;
        [SerializeField]
        private StartCanvas _start = null;

        [SerializeField]
        private bool _isOnline = false;

        private Dictionary<SignInStates, ISignInState> states;
        private ISignInState currentState;


        protected override void Awake()
        {
            base.Awake();

            states = new Dictionary<SignInStates, ISignInState>
            {
                { SignInStates.Register, new RegisterState(_register) },
                { SignInStates.Login, new LoginState(_login) },
                { SignInStates.Start, new StartState(_start) }

            };

        }

        private void Start()
        {
            if (_isOnline)
            {
                if (IsLoggedIn())
                    ChangeState(SignInStates.Start);
                else
                    ChangeState(SignInStates.Login);
            }
            else ChangeState(SignInStates.Start);
            
        }

        public void ChangeState(SignInStates newState)
        {
            if (currentState != null)
                currentState.OnExit();

            currentState = states[newState];
            currentState.OnEnter();
        }

        private void OnPlayerLoggedIn()
        {
            ChangeState(SignInStates.Start);
            SetLoggedIn(true);
        }

        private void SetLoggedIn(bool value)
        {
            if (value == true)
            {
                PlayerPrefs.SetInt("loggedIn", 1);
            }
            else
            {
                PlayerPrefs.SetInt("loggedIn", 0);
            }
        }

        public bool IsLoggedIn()
        {
            return PlayerPrefs.GetInt("loggedIn") == 1;
        }

        private void OnApplicationQuiting()
        {
            SetLoggedIn(false);
        }

        void OnEnable()
        {
            Register.OnUserRegistered += OnPlayerLoggedIn;
            Login.LoggedIn += OnPlayerLoggedIn;
            Application.quitting += OnApplicationQuiting;
        }

        void OnDisable()
        {
            Register.OnUserRegistered -= OnPlayerLoggedIn;
            Login.LoggedIn -= OnPlayerLoggedIn;
            Application.quitting -= OnApplicationQuiting;
        }




    }


}

