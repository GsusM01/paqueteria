using paqueteservidor.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500", "https://paqueteria-cliente-dx19.onrender.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) { app.MapOpenApi(); }

app.UseHttpsRedirection();

app.UseWebSockets();

app.UseCors("PermitirFrontend");

app.MapGet("/", () => "Servidor de paquetería funcionando");

app.MapHub<PaqueteriaHub>("/paquetes");

app.Run();
