using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charity.Network.ObjectProtocol
{
    [Serializable]
    public class LoginUserRequest : IRequest
    {
        public string Username { get; }
        public string Password { get; }

        public LoginUserRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
