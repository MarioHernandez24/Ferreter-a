using SistemsFerreDeSur.Shared;
using System.Net.Http.Json;

namespace SistemsFerreDeSur.Client.Services
{
    public class ProveedorService : IProveedorService
    {

        private readonly HttpClient _http;
        public ProveedorService(HttpClient http) 
        {
            _http = http;
        }
        public async Task<List<ProveedorDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<ProveedorDTO>>>("api/Proveedor/Lista");

            if (result!.EsCorrecto)
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
                return result.Valor;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<ProveedorDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<ProveedorDTO>>($"api/Proveedor/Buscar/{id}");

            if (result!.EsCorrecto)
#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
                return result.Valor;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(ProveedorDTO proveedor)
        {
            var result = await _http.PostAsJsonAsync("api/Proveedor/Guardar",proveedor);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();   

            if (response!.EsCorrecto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(ProveedorDTO proveedor)
        {
            var result = await _http.PutAsJsonAsync($"api/Proveedor/Editar/{proveedor.IdProveedor}", proveedor);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {

            var result = await _http.DeleteAsync($"api/Proveedor/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto;
            else
                throw new Exception(response.Mensaje);
        }
        
    }
}
