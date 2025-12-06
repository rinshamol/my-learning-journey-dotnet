using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
List<Item> items = new List<Item>();
app.UseHttpsRedirection();
app.MapGet("/", () => "Welcome to the Simple Web API!");
app.MapGet("/item", () => items);
app.MapGet("/item/{id}", (int id) =>
{
    var item = items.FirstOrDefault(p => p.Id == id);
    return item != null ? Results.Ok(item) : Results.NotFound();
});

app.MapPost("/item", (Item item) =>
{
    items.Add(item);
    return Results.Ok(item);
});
app.MapPut("/item/{id}", (int id, Item updatedItem) =>
{
    var item = items.FirstOrDefault(p => p.Id == id);
    if(item == null) return Results.NotFound(); 
    item.Name = updatedItem.Name;
    return Results.Ok(item);
    
});

app.MapDelete("/item/{id}", (int id) =>
{
    items.RemoveAt(id);
    return Results.NoContent();   
});


app.Run();

