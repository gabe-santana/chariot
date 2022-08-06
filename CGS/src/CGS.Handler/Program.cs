using CGS.Config;
using CGS.Handler.Config;

#region Builder
var builder = WebApplication.CreateBuilder(args);

builder.ConfigureCors();
builder.ConfigureSwagger();
builder.ConfigureIoC();
#endregion

#region App
var app = builder.Build();

app.AddHttp();
app.AddCors();
app.AddWebSocket();
app.AddMidlewares();

app.Run();
#endregion