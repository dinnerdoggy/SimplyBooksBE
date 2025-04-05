using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<SimplyBooksBEDbContext>(builder.Configuration["SimplyBooksBEDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ***** AUTHOR ENDPOINTS ******

// GET Authors
app.MapGet("author", (SimplyBooksBEDbContext db) =>
{
    return Results.Ok(db.Authors);
});

// GET Author by Id
app.MapGet("author/{id}", (SimplyBooksBEDbContext db, int id) =>
{
    try
    {
        var AS = db.Authors
        .Include(a => a.Books)
        .FirstOrDefault(a => a.Id == id);
        return Results.Ok(AS);
    }
    catch
    {
        return Results.NotFound("No author found");
    }
});

// ***** BOOK ENDPOINTS ******

// GET Books
app.MapGet("book", (SimplyBooksBEDbContext db) =>
{
    try
    {
        var BA = db.Books
        .Include(b => b.Author)
        .ToList();
        return Results.Ok(BA);
    }
    catch
    {
        return Results.NotFound("No Books Found");
    }
});

app.Run();
