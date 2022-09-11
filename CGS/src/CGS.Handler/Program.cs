using CGS.Config;
using CGS.Handler.Config;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCors();
builder.ConfigureSwagger();
builder.ConfigureIoC();
builder.ConfigureCache(builder.Configuration.GetConnectionString("Redis"));
#endregion

#region App
var app = builder.Build();

app.AddHttp();
app.AddCors();
app.AddWebSocket();

app.MapSockets("/ws");

app.Run();
#endregion