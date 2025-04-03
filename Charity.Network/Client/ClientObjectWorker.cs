using System.Net.Sockets;
using Charity.Network.DTO;
using Charity.Network.ObjectProtocol;
using Charity.Service;

namespace Charity.Network.Client;

public class ClientObjectWorker : ClientObjectWorkerBase
{
    IAppService service;
    public ClientObjectWorker(IAppService service, TcpClient connection) : base(connection)
    {
        this.service = service;
    }

    protected override IResponse HandleRequest(IRequest request)
    {
        if (request is LoginUserRequest)
        {
            var username = (request as LoginUserRequest).Username;
            var password = (request as LoginUserRequest).Password;
            return ResponseOrError(() =>
            {
                var user = service.Login(username, password);
                return new LoginUserResponse(UserDTO.FromUser(user));
            });
        }
        if (request is GetCazuriRequest)
        {
            return ResponseOrError(() => new GetCazuriResponse(service.GetAllCazuri().Select(c => CazCaritabilDTO.FromCazCaritabil(c)).ToArray()));
        }
        return new ErrorResponse("Unknown request");
    }

    private IResponse ResponseOrError(Func<IResponse> func)
    {
        try
        {
            return func();
        }
        catch (Exception e)
        {
            return new ErrorResponse(e.Message);
        }
    }
}