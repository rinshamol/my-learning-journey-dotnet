
public class MyService : IMyService
{
    private readonly int _serviceId;

    public MyService()
    {
        _serviceId = new Random().Next(100000,9999999);
    }

 

    public void LogCreation(string message)
    {
        Console.WriteLine($"{message} servicd ID : {_serviceId}");
    }
}