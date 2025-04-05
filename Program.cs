using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SimplyBooksBE.Models;

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

// CREATE Author
app.MapPost("/author", async (SimplyBooksBEDbContext db, Author newAuthor) =>
{
    // Trim strings before validating
    newAuthor.FirstName = newAuthor.FirstName?.Trim();
    newAuthor.LastName = newAuthor.LastName?.Trim();
    newAuthor.Email = newAuthor.Email?.Trim();
    newAuthor.Image = newAuthor.Image?.Trim();
    newAuthor.Uid = newAuthor.Uid?.Trim();

    try
    {
        db.Authors.Add(newAuthor);
        await db.SaveChangesAsync();
        return Results.Created($"/author/{newAuthor.Id}", newAuthor);
    }
    catch
    {
        return Results.Problem("Error creating author");
    }
});


// ***** BOOK ENDPOINTS ******

// GET Books
app.MapGet("book", (SimplyBooksBEDbContext db) =>
{
    return Results.Ok(db.Books);
});

// GET Book by Id
app.MapGet("book/{id}", (SimplyBooksBEDbContext db, int id) =>
{
    try
    {
        var BA = db.Books
        .Include(b => b.Author)
        .FirstOrDefault(b => b.Id == id);
        return Results.Ok(BA);
    }
    catch
    {
        return Results.NotFound("Fix your code");
    }
});

// CREATE Book
app.MapPost("/book", async (SimplyBooksBEDbContext db, Book newBook) =>
{
    // Check for required fields
    if (string.IsNullOrWhiteSpace(newBook.Title) || newBook.AuthorId <= 0)
    {
        return Results.BadRequest("Missing required fields: Title and AuthorId.");
    }

    // Validate that the author exists
    var authorExists = await db.Authors.AnyAsync(a => a.Id == newBook.AuthorId);
    if (!authorExists)
    {
        return Results.BadRequest($"Author with ID {newBook.AuthorId} does not exist.");
    }

    try
    {
        db.Books.Add(newBook);
        await db.SaveChangesAsync();
        return Results.Created($"/book/{newBook.Id}", newBook);
    }
    catch (Exception ex)
    {
        return Results.Problem("Error creating book: " + ex.Message);
    }
});


app.Run();
