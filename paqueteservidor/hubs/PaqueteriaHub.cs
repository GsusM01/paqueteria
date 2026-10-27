using Microsoft.AspNetCore.SignalR;

namespace paqueteservidor.Hubs
{
    // Un Hub es como el "grupo de WhatsApp": recibe lo que manda un cliente y lo reenvía.
    public class PaqueteriaHub : Hub
    {
        // ===== 1. REGISTRAR UN ENVÍO NUEVO =====
        // HTML: conexion.invoke("NuevoEnvio", origenLat, origenLng, destLat, destLng, precio, tipo)
        public async Task NuevoEnvio(double origenLat, double origenLng, double destLat, double destLng, string precio, string tipo)
        {
            // HTML: conexion.on("RecibirEnvio", (origenLat, origenLng, destLat, destLng, precio, tipo) => ...)
            await Clients.All.SendAsync("RecibirEnvio", origenLat, origenLng, destLat, destLng, precio, tipo);
        }

        // ===== 2. AGREGAR UNA PARADA O ENTREGAR =====
        // HTML: conexion.invoke("ActualizarRuta", lat, lng, estado)   estado = "En ruta" o "Entregado"
        public async Task ActualizarRuta(double lat, double lng, string estado)
        {
            // HTML: conexion.on("RecibirActualizacion", (lat, lng, estado) => ...)
            await Clients.All.SendAsync("RecibirActualizacion", lat, lng, estado);
        }

        // Para que solo le llegue a uno, cambia "All" por:
        //   Clients.Caller  -> solo al que lo mandó
        //   Clients.Others  -> a todos menos al que lo mandó
    }
}
