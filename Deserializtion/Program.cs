using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/auto", (Person personFromClient) =>
{
    return TypedResults.Ok(personFromClient);
});

app.MapPost("/json", async (HttpContext context) =>
{
    var person = await context.Request.ReadFromJsonAsync<Person>();
    Console.WriteLine(person);
    return TypedResults.Json(person);
});

app.MapPost("/custom", async (HttpContext context) =>
{   
    var options = new JsonSerializerOptions
    {
        UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
    };
    var person = await context.Request.ReadFromJsonAsync<Person>(options);
    return TypedResults.Json(person);
});
app.MapPost("/xml", async (HttpContext context) =>
{
   var reader = new StreamReader(context.Request.Body);
   var body = await reader.ReadToEndAsync();
   var xmlSerializer = new XmlSerializer(typeof(Person));
   var stringReader = new StringReader(body);
   var person = xmlSerializer.Deserialize(stringReader);
   return TypedResults.Ok(person);
});
app.Run();

public class Person
{
    required public string UserName { get; set; }
    public int? UserAge { get; set; }
}