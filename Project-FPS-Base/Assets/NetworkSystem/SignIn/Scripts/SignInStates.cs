using System;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    public enum SignInStates
    {
        None,
        Register,
        Login
    }

    public class RegisterState : ISignInState
    {
        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public void OnUpdate()
        {
        }
    }

    public class LoginState : ISignInState
    {
        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }

        public void OnUpdate()
        {
        }
    }

    public interface ISignInState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}
