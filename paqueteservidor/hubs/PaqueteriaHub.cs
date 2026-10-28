using Microsoft.AspNetCore.SignalR;

namespace paqueteservidor.Hubs
{
    public class PaqueteriaHub : Hub
    {
        public async Task NuevoEnvio(double origenLat, double origenLng, double destLat, double destLng, string precio, string tipo, string fechaEntrega)
        {
            string fechaCreacion = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            await Clients.All.SendAsync("RecibirEnvio", origenLat, origenLng, destLat, destLng, precio, tipo, fechaEntrega, fechaCreacion);
        }

        public async Task ActualizarRuta(double lat, double lng)
        {
            string fecha = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            await Clients.All.SendAsync("RecibirActualizacion", lat, lng, fecha);
        }

        public async Task CambiarEstado(string estado)
        {
            string fecha = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            await Clients.All.SendAsync("EstadoCambiado", estado, fecha);
        }

    }
}
