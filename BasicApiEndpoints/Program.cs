var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello Rinsha!");
app.MapGet("/hello", () => "Hello Aadhil!");
app.MapPost("/postMethod", () => "Post Method");


//Routing parameter
app.MapGet("/users/{id}/posts/{slug}", (int id, string slug) =>{
    return $"userId: {id} and posts: {slug}";
});

//Route constraint
app.MapGet("/products/{id:int:min(0)}",(int id) => {
    return $"product ID: {id}";
});

//optional parameter
app.MapGet("/report/{year?}", (int year = 2026) =>{
    return $"Report year: {year}";
});

//catch-all route using wild-card(*)
app.MapGet("file/{*filepath}", (string filepath) =>{
    return $"filepath: {filepath}";
});

//Query parameters
app.MapGet("/search", (string? q, int page = 1) =>{
    return $" Search query: {q}, page: {page}";
});

app.MapGet("mixture/{id}/{name?}/{*imagepath}", (int id, int data, string imagepath,string name = "Rinsha") =>
{
    return $"Mixture contains ID:{id},name: {name}, image path: {imagepath}, data: {data}";  
});


app.Run();
