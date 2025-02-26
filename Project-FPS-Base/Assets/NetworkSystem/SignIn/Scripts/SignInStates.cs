using Assets.MenuSystem.Scripts;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    public enum SignInStates
    {
        None,
        Register,
        Login,
        Start
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

        public void OnExit()
        {
            _login.gameObject.SetActive(false);
        }
    }

    public class StartState : ISignInState
    {
        private readonly StartCanvas _start;
        public StartState(StartCanvas start)
        {
            _start = start;
        }

        public void OnEnter()
        {
            _start.gameObject.SetActive(true);
        }

        public void OnExit()
        {
            _start.gameObject.SetActive(false);
        }
    }

    public interface ISignInState
    {
        void OnEnter();
        void OnExit();
    }
}
