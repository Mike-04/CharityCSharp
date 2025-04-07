using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charity.Network.ObjectProtocol
{
    [Serializable]
    public class LogoutUserRequest : IRequest
    {
        public string Username { get; }

        public LogoutUserRequest(string username)
        {
            Username = username;
        }
    }
}
