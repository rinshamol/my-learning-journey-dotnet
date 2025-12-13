using Microsoft.AspNetCore.Http.HttpResults;
var builder = WebApplication.CreateBuilder(args);
// builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var blogs = new List<Blog>
{
    new Blog { Title = "My First Blog", Body = "This is my first post"},
    new Blog { Title = "My Second Blog", Body = "This is my Second post"}
};

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// implementing custom middleware
app.Use(async(context, next) =>
{   
    var statTime = DateTime.UtcNow;
    Console.WriteLine($"start time: {statTime}");
    await next.Invoke();
    var duration = DateTime.UtcNow - statTime;
    Console.WriteLine($"duration: {duration}");

});
app.Use(async (context, next) =>
{   //code before
    Console.WriteLine(context.Request.Path);
    await next.Invoke();
    //code after
    Console.WriteLine(context.Response.StatusCode);
});

app.UseWhen(
    context => context.Request.Method != "GET",
    appBuilder => appBuilder.Use(async(context, next) =>
    {
        var extractedPassword = context.Request.Headers["X-Api-Key"];
        if(extractedPassword == "thisIsBadPassword")
        {
            await next.Invoke();
       } else
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid Api kay");
        }
    })
);

app.MapGet("/", () => "Hello World!").ExcludeFromDescription();
app.MapGet("/blog", () =>
{
    return blogs;
});

app.MapGet("/blog/{id}",
[EndpointDescription("Return a single blog")]
[EndpointSummary("Get single blog")]
 Results<Ok<Blog>,NotFound> (int id) =>
{
    if ( id < 0 || id >= blogs.Count)
    {
        return TypedResults.NotFound();
    }else
    {
        return TypedResults.Ok(blogs[id]);  
    }
});

app.MapPost("/blog", (Blog blog) =>
{
    blogs.Add(blog);
    return Results.Created($"/blog/{blogs.Count - 1}", blog);
});
app.MapPost("/blog/api", (Blog blog) =>
{
    blogs.Add(blog);
    return Results.Created($"/blog/{blogs.Count - 1}", blog);
});
app.MapDelete("/blog/{id}", (int id) =>
{
    if ( id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }else
    {
        blogs.RemoveAt(id);
        return Results.NoContent();  
    }
});
app.MapPut("/blog/{id}", (int id, Blog blog) =>
{
    if ( id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }else
    {
        blogs[id] = blog;
        return Results.Ok(blog);  
    }
});

app.Run();
public class Blog
{
    public required string Title { get;set; }
    public required string Body { get;set; }
}