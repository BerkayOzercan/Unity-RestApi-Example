using System;

namespace Assets.NetworkSystem.Register.Scripts
{
    [Serializable]
    public class RegisterRequest
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}


