var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Rinsha!");
app.MapGet("/hello", () => "Hello Aadhil!");
app.MapPost("/postMethod", () => "Post Method");

app.Run();
