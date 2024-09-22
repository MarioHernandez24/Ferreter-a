using SistemsFerreDeSur.Shared;
using System.Net.Http.Json;

namespace SistemsFerreDeSur.Client.Services
{
    public interface IUsuarioPermisoService
    {
        Task<List<UsuarioPermisoDTO>> GetUsuarioPermisos(int userId);
        Task CreateUsuarioPermiso(UsuarioPermisoDTO usuarioPermisoDTO);
    }

    public class UsuarioPermisoService : IUsuarioPermisoService
    {
        private readonly HttpClient _httpClient;

        public UsuarioPermisoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UsuarioPermisoDTO>> GetUsuarioPermisos(int userId)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseAPI<List<UsuarioPermisoDTO>>>($"api/usuarioPermiso/buscar/{userId}");
            return response?.Valor;
        }

        public async Task CreateUsuarioPermiso(UsuarioPermisoDTO usuarioPermisoDTO)
        {
            await _httpClient.PostAsJsonAsync("api/usuarioPermiso/asignar", usuarioPermisoDTO);
        }
    }
}

