using Charity.Network.DTO;
using System;

namespace Charity.Network.ObjectProtocol
{
    [Serializable]
    public class LoginUserResponse : IResponse
    {
        public UserDTO User { get; }
        public LoginUserResponse(UserDTO user) => User = user;
    }
}
