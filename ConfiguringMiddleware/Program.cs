var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging((o) => {});
var app = builder.Build();

//built-in middleware before my code

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseExceptionHandler();
//custom middleware
app.Use(async (context, next) =>
{
    Console.WriteLine("Logic before");
    await next.Invoke();
    Console.WriteLine("Logic After");
});
//built-in middleware after my code
app.UseHttpLogging();

app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => "Hello from another router!");


app.Run();
