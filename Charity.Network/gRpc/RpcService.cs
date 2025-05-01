using System.Collections.Concurrent;
using Charity.Proto;
using Charity.Service;
using Charity.Service.Observer;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Charity.Network.gRpc;

public class RpcService(Service.Service service) : CharityService.CharityServiceBase
{
    private readonly Service.Service _service = service;
    
    private readonly ConcurrentDictionary<string, IServerStreamWriter<UpdateResponse>> _observers = new();

    public override async Task SubscribeToNotifications(Empty request, IServerStreamWriter<UpdateResponse> responseStream, ServerCallContext context)
    {
        string clientId = Guid.NewGuid().ToString();
        _observers.TryAdd(clientId, responseStream);

        try
        {
            // Keep the subscription alive until the client disconnects
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000); // Keep the loop alive
            }
        }
        finally
        {
            // Remove the client when disconnected
            _observers.TryRemove(clientId, out _);
        }
    }

    public async Task NotifyClients(UpdateResponse update)
    {
        foreach (var observer in _observers.Values)
        {
            try
            {
                await observer.WriteAsync(update);
            }
            catch
            {
                // Handle disconnected clients
            }
        }
    }
    

    public override Task<LoginUserResponse> LoginUser(LoginUserRequest request, ServerCallContext context)
    {
        var user = _service.Login(request.Username, request.Password);

        // Log the current observers

        return Task.FromResult(new LoginUserResponse
        {
            User = new User
            {
                Id = user.Id.ToString(),
                Username = user.Username,
                Password = user.PasswordHash
            }
        });
    }
    
    public override Task<GetCazuriResponse> GetCazuri(GetCazuriRequest request, ServerCallContext context)
    {
        var cazuri = _service.GetAllCazuri();
        var response = new GetCazuriResponse();
        response.Cazuri.AddRange(cazuri.Select(c => new CazCaritabil
        {
            Id = c.Id.ToString(),
            Nume = c.Nume,
            SumaAdunata = c.SumaAdunata
        }));
        return Task.FromResult(response);
    }
    
    public override Task<GetDonatorsResponse> GetDonators(GetDonatorsRequest request, ServerCallContext context)
    {
        var donators = _service.GetDonators(request.SearchString);
        var response = new GetDonatorsResponse();
        response.Donators.AddRange(donators.Select(d => new Donator
        {
            Id = d.Id.ToString(),
            Nume = d.Nume,
            Adresa = d.Adresa,
            NumarTelefon = d.NumarTelefon
        }));
        return Task.FromResult(response);
    }
    
    public override Task<OkResponse> AddDonation(AddDonationRequest request, ServerCallContext context)
    {
        Domain.Donator selectedDonator = new Domain.Donator(Guid.Parse(request.Donator.Id),
            request.Donator.Nume,
            request.Donator.Adresa,
            request.Donator.NumarTelefon);

        Domain.CazCaritabil selectedCase = new Domain.CazCaritabil(Guid.Parse(request.CazCaritabil.Id),
            request.CazCaritabil.Nume, request.CazCaritabil.SumaAdunata);
        _service.addDonation(selectedDonator, selectedCase, request.SumaDonata);
        NotifyClients(new UpdateResponse());
        return Task.FromResult(new OkResponse());
    }
    
    public override Task<OkResponse> AddDonator(AddDonatorRequest request, ServerCallContext context)
    {
        _service.AddDonator(request.Nume, request.Adresa, request.NumarTelefon);
        NotifyClients(new UpdateResponse());
        return Task.FromResult(new OkResponse());
    }
    
    public override Task<OkResponse> UpdateDonator(UpdateDonatorRequest request, ServerCallContext context)
    {
        _service.UpdateDonator(Guid.Parse(request.Id), request.Nume, request.Adresa, request.NumarTelefon);
        NotifyClients(new UpdateResponse());
        return Task.FromResult(new OkResponse());
    }
    
    public override  Task<OkResponse> AddCazCaritabil(AddCazCaritabilRequest request, ServerCallContext context)
    {
        _service.AddCazCaritabil(request.Nume, request.Suma);
        NotifyClients(new UpdateResponse());
        return Task.FromResult(new OkResponse());
    }
    
    public override Task<OkResponse> UpdateCazCaritabil(UpdateCazCaritabilRequest request, ServerCallContext context)
    {
        _service.UpdateCazCaritabil(Guid.Parse(request.Id), request.Nume, request.Suma);
        NotifyClients(new UpdateResponse());
        return Task.FromResult(new OkResponse());
    }

    public override Task<OkResponse> LogoutUser(LogoutUserRequest request, ServerCallContext context)
    {
        return Task.FromResult(new OkResponse());
    }
    
}