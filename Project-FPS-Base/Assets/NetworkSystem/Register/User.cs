using System;

namespace Assets.NetworkSystem.Register.Scripts
{
    [Serializable]
    public class User
    {
        public string username;
        public string email;
        public string password;
    }

    [Serializable]
    public class LoginUser
    {
        public string username;
        public string password;
    }
}


