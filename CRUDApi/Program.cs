var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var blogs = new List<Blog>
{
    new Blog { Title = "My First Blog", Body = "This is my first post"},
    new Blog { Title = "My Second Blog", Body = "This is my Second post"}
};

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

app.MapGet("/", () => "Hello World!");
app.MapGet("/blog", () =>
{
    return blogs;
});
app.MapGet("/blog/{id}", (int id) =>
{
    if ( id < 0 || id >= blogs.Count)
    {
        return Results.NotFound();
    }else
    {
        return Results.Ok(blogs[id]);  
    }
});

app.MapPost("/blog", (Blog blog) =>
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