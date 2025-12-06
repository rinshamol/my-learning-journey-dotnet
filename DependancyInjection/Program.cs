var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddScoped<IMyService,MyService>();
// builder.Services.AddSingleton<IMyService,MyService>();
builder.Services.AddTransient<IMyService,MyService>();


var app = builder.Build();
app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.LogCreation("First middleware");
    await next.Invoke();
});

app.MapGet("/",(IMyService myService) =>
{
    myService.LogCreation("Root");
    return Results.Ok("check the console for service creation log");
});
app.Run();

public interface IMyService
{
    void LogCreation(string message);
}

public class MyService : IMyService
{
    private readonly int _serviceId;
    public MyService()
    {
        _serviceId = new Random().Next(100000,9999999);
    }

    public void LogCreation(string message)
    {
        Console.WriteLine($"{message} serviceId: {_serviceId}");
    }
}