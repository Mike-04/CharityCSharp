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

        if (request is GetDonatorsRequest)
        {
            String searchString = (request as GetDonatorsRequest).SearchString;
            return ResponseOrError(() => new GetDonatorsResponse(service.GetDonators(searchString).Select(d => DonatorDTO.FromDonator(d)).ToArray()));
        }
        if (request is AddDonationRequest)
        {
            var selectedDonor = (request as AddDonationRequest).Donator.ToDonator();
            var selectedCase = (request as AddDonationRequest).CazCaritabil.ToCazCaritabil();
            var v = (request as AddDonationRequest).SumaDonata;
            return ResponseOrError(() =>
            {
                service.addDonation(selectedDonor, selectedCase, v);
                return new OkResponse();

            });
        }
        if (request is AddDonatorRequest)
        {
            var name = (request as AddDonatorRequest).Nume;
            var telefon = (request as AddDonatorRequest).NumarTelefon;
            var adresa = (request as AddDonatorRequest).Adresa;
            return ResponseOrError(() =>
            {
                service.AddDonator(name, adresa, telefon);
                return new OkResponse();
            });
        }
        if (request is UpdateDonatorRequest)
        {
            var id = (request as UpdateDonatorRequest).Id;
            var name = (request as UpdateDonatorRequest).Nume;
            var telefon = (request as UpdateDonatorRequest).NumarTelefon;
            var adresa = (request as UpdateDonatorRequest).Adresa;
            return ResponseOrError(() =>
            {
                service.UpdateDonator(id, name, adresa, telefon);
                return new OkResponse();
            });
        }

        if (request is AddCazCaritabilRequest)
        {
            var name = (request as AddCazCaritabilRequest).Nume;
            var sumaAdunata = (request as AddCazCaritabilRequest).Suma;
            return ResponseOrError(() =>
            {
                service.AddCazCaritabil(name, sumaAdunata);
                return new OkResponse();
            });
        }
        
        if (request is UpdateCazCaritabilRequest)
        {
            var id = (request as UpdateCazCaritabilRequest).Id;
            var name = (request as UpdateCazCaritabilRequest).Nume;
            var sumaAdunata = (request as UpdateCazCaritabilRequest).Suma;
            return ResponseOrError(() =>
            {
                service.UpdateCazCaritabil(id, name, sumaAdunata);
                return new OkResponse();
            });
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