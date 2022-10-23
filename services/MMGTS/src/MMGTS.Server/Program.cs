using MediatR;
using MMGTS.Server.Config;
using MMGTS.Server.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

#region Builder

// Build gRPC
builder.Services.AddGrpc();

// Build Mediator class
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Build database connection configuration
builder.Services.ConfigureDatabase(config.GetConnectionString("Postgres"));

// Build app
var app = builder.Build();

#endregion

#region app
app.MapGrpcService<DaprService>();
app.ConfigureMigration();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client");

app.Run();
#endregion
