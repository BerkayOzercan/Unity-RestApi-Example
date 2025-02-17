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

        private Dictionary<SignInStates, ISignInState> states;
        private ISignInState currentState;


        protected override void Awake()
        {
            base.Awake();

            states = new Dictionary<SignInStates, ISignInState>
            {
                { SignInStates.Register, new RegisterState(_register) },
                { SignInStates.Login, new LoginState(_login) },
            };

            ChangeState(SignInStates.Login);
        }

        private void Update()
        {
            currentState?.OnUpdate();
        }

        public void ChangeState(SignInStates newState)
        {
            if (currentState != null)
                currentState.OnExit();

            currentState = states[newState];
            currentState.OnEnter();
        }


    }


}

