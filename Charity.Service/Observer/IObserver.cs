namespace Charity.Service.Observer;

public interface IObserver
{
    void Observe();
    void Notify(string message);
}