using Microsoft.AspNetCore.SignalR;

namespace paqueteservidor.Hubs
{
    // Un Hub es como el "grupo de WhatsApp": recibe lo que manda un cliente y lo reenvía.
    public class PaqueteriaHub : Hub
    {
        // ===== 1. REGISTRAR UN ENVÍO NUEVO =====
        // HTML: conexion.invoke("NuevoEnvio", origenLat, origenLng, destLat, destLng, precio, tipo, fechaEntrega)
        public async Task NuevoEnvio(double origenLat, double origenLng, double destLat, double destLng, string precio, string tipo, string fechaEntrega)
        {
            // el servidor pone la fecha en que se creó (como la hora del chat)
            string fechaCreacion = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            // HTML: conexion.on("RecibirEnvio", (origenLat, origenLng, destLat, destLng, precio, tipo, fechaEntrega, fechaCreacion) => ...)
            await Clients.All.SendAsync("RecibirEnvio", origenLat, origenLng, destLat, destLng, precio, tipo, fechaEntrega, fechaCreacion);
        }

        // ===== 2. AGREGAR UN PUNTO A LA RUTA (botón Modificar → clic en el mapa) =====
        // HTML: conexion.invoke("ActualizarRuta", lat, lng)
        public async Task ActualizarRuta(double lat, double lng)
        {
            // el servidor pone la fecha y hora en que se agregó el punto
            string fecha = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            // HTML: conexion.on("RecibirActualizacion", (lat, lng, fecha) => ...)
            await Clients.All.SendAsync("RecibirActualizacion", lat, lng, fecha);
        }

        // ===== 3. CAMBIAR EL ESTADO (Recibido, En tránsito, Entregado) =====
        // HTML: conexion.invoke("CambiarEstado", estado)
        public async Task CambiarEstado(string estado)
        {
            // el servidor pone la fecha del cambio (para el historial)
            string fecha = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            // HTML: conexion.on("EstadoCambiado", (estado, fecha) => ...)
            await Clients.All.SendAsync("EstadoCambiado", estado, fecha);
        }

        // Para que solo le llegue a uno, cambia "All" por:
        //   Clients.Caller  -> solo al que lo mandó
        //   Clients.Others  -> a todos menos al que lo mandó
    }
}
