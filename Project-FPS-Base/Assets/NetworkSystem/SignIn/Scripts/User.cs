using System;

namespace Assets.NetworkSystem.SignIn.Scripts
{
    [Serializable]
    public class User
    {
        public string id;
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


