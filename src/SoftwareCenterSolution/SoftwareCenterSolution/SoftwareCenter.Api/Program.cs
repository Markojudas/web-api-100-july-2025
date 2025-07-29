using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("db") ?? 
    throw new Exception("Need a connection string");

Console.WriteLine("Using Connection String: " + connectionString);
builder.Services.AddMarten(config =>
{
    config.Connection(connectionString);

}).UseLightweightSessions();

// it will give us a scoped service called IDocumentSession
// if this was Entity framework, it would give us a "DbContext" object we can use.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
// everything after this is configuring "middleware"
// that is is stuff that will intercept incoming our outgoing HTTP requests
// and process them in some way.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers(); // Go find all the controllers and look at the attributes (HttpGet, HttpPost, etc.)
// and make yourself a cheat sheet.
// if I get a POST /vendors - create a Vendors/Controller instance, and call the AddVendorAsync Method.
Console.WriteLine("Fixing to run your API");
app.Run(); // this is a "blocking method" basically a while(true) {... }
Console.WriteLine("done running your API");

public partial class Program; // In .NET 10 you won't have to do this.