using Charity.Network.DTO;
using System;
using Charity.Network.DTO;

namespace Charity.Network.ObjectProtocol
{
    [Serializable]
    public class LoginUserResponse : IResponse
    {
        public UserDTO User { get; }
        public LoginUserResponse(UserDTO user) => User = user;
    }
}
