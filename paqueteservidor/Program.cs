using paqueteservidor.Hubs;   // para poder usar PaqueteriaHub

var builder = WebApplication.CreateBuilder(args);

// ================= ANTES DEL Build: "voy a usar estas herramientas" =================

// OpenApi (documentación de la API, viene de la plantilla webapi)
builder.Services.AddOpenApi();

// 1. Activar SignalR. Ya viene incluido en ASP.NET Core: no se instala ningún paquete
builder.Services.AddSignalR();

// 2. CORS: la lista de páginas que se pueden conectar (Live Server corre en el puerto 5500).
//    AllowCredentials es obligatorio para SignalR: sin él, la conexión WebSocket no se hace
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

// ================= DESPUÉS DEL Build: "así se configuran" =================

// En desarrollo publica la documentación en /openapi/v1.json
if (app.Environment.IsDevelopment()) { app.MapOpenApi(); }

app.UseHttpsRedirection();

// 3. Habilitar WebSockets de forma explícita: es el transporte que SignalR negocia primero
app.UseWebSockets();

// 4. Usar el permiso de CORS (el nombre es IGUAL al del AddPolicy)
app.UseCors("PermitirFrontend");

// 5. Endpoint de prueba: al abrir http://localhost:5117/ debe salir este texto
app.MapGet("/", () => "Servidor de paquetería funcionando");

// 6. Ruta del hub. URL completa = puerto de launchSettings.json + "/paquetes"
//    En el HTML: .withUrl("http://localhost:5117/paquetes")
app.MapHub<PaqueteriaHub>("/paquetes");

app.Run();
