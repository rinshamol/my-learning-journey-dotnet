var builder = WebApplication.CreateBuilder(args);


// builder.Services.AddHttpLogging((o) => {});
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
var app = builder.Build();
//Configure exception handling middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}else
{
    app.UseDeveloperExceptionPage();
}

//built-in middleware before my code

app.UseAuthentication();
app.UseAuthorization();
// Add HTTP logging middleware
app.UseHttpLogging();
// app.UseRouting();

// routes
app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => "Hello from another router!");

//custom middleware
app.Use(async (context, next) =>
{
    Console.WriteLine("Logic before");
    Console.WriteLine($"Request path: {context.Request.Path}");
    await next.Invoke();
    Console.WriteLine($"Response status code: {context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    var startTime = DateTime.UtcNow;
    Console.WriteLine($"start time: {startTime}");
    await next.Invoke();
    Console.WriteLine("Code after");
    var duration = DateTime.UtcNow - startTime;
    Console.WriteLine($"dauration: {duration.TotalMilliseconds} ms");

});

app.Run();
