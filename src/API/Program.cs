using API.Configurations;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Loading all configurations from
// JSON files
// Environment variables
builder.Host.AddConfigurations();


builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseInfrastructure(builder.Configuration);
app.MapEndpoints();

app.Run();
