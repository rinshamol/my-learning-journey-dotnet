// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Remove HTTPS redirection so you can test with http

// app.UseHttpsRedirection();

app.UseAuthentication();
app.MapControllers();
app.Run();

