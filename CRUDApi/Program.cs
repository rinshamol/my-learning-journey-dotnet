var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var blogs = new List<Blog>
{
    new Blog { Title = "My First Blog", Body = "This is my first post"},
    new Blog { Title = "My Second Blog", Body = "This is my Second post"}
};

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