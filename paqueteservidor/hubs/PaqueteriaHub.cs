using Microsoft.AspNetCore.SignalR;

namespace servidormapa 
{
    public class PaqueteriaHub : Hub
    {
        // Método para registrar el paquete por primera vez
        public async Task NuevoEnvio(string origenLat, string origenLng, string destLat, string destLng, string precio, string tipo)
        {
            await Clients.All.SendAsync("RecibirEnvio", origenLat, origenLng, destLat, destLng, precio, tipo);
        }

        // Método para que la Persona B agregue nuevos puntos a la ruta después
        public async Task ActualizarRuta(string nuevaLat, string nuevaLng, string estado)
        {
            await Clients.All.SendAsync("RecibirActualizacion", nuevaLat, nuevaLng, estado);
        }
    }
}