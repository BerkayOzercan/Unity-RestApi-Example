using System.Collections.Generic;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    public class SignInManager : Singleton<SignInManager>
    {
        private Dictionary<SignInStates, ISignInState> states;
        private ISignInState currentState;


        protected override void Awake()
        {
            base.Awake();

            states = new Dictionary<SignInStates, ISignInState>
            {
                { SignInStates.Register, new RegisterState() },
                { SignInStates.Login, new LoginState() },
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

