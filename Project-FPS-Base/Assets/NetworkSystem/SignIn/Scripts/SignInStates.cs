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
        private readonly Register _register;
        public RegisterState(Register register)
        {
            _register = register;
        }
        public void OnEnter()
        {
            _register.gameObject.SetActive(true);
        }

        public void OnUpdate() { }

        public void OnExit()
        {
            _register.gameObject.SetActive(false);
        }
    }

    public class LoginState : ISignInState
    {
        private readonly Login _login;
        public LoginState(Login login)
        {
            _login = login;
        }

        public void OnEnter()
        {
            _login.gameObject.SetActive(true);
        }

        public void OnUpdate() { }

        public void OnExit()
        {
            _login.gameObject.SetActive(false);
        }
    }

    public interface ISignInState
    {
        void OnEnter();
        void OnUpdate();
        void OnExit();
    }
}
