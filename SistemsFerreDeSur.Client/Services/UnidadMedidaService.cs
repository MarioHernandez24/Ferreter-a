using SistemsFerreDeSur.Shared;
using System.Net.Http.Json;

namespace SistemsFerreDeSur.Client.Services
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly HttpClient _http;

        public UnidadMedidaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<UnidadMedidaDTO>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<List<UnidadMedidaDTO>>>("api/UnidadMedida/Lista");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<UnidadMedidaDTO> Buscar(int id)
        {
            var result = await _http.GetFromJsonAsync<ResponseAPI<UnidadMedidaDTO>>($"api/UnidadMedida/Buscar/{id}");

            if (result!.EsCorrecto)
                return result.Valor!;
            else
                throw new Exception(result.Mensaje);
        }

        public async Task<int> Guardar(UnidadMedidaDTO unidadMedida)
        {
            var result = await _http.PostAsJsonAsync("api/UnidadMedida/Guardar", unidadMedida);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<int> Editar(UnidadMedidaDTO unidadMedida)
        {
            var result = await _http.PutAsJsonAsync($"api/UnidadMedida/Editar/{unidadMedida.IdUnidad}", unidadMedida);
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.Valor;
            else
                throw new Exception(response.Mensaje);
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/UnidadMedida/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

            if (response!.EsCorrecto)
                return response.EsCorrecto;
            else
                throw new Exception(response.Mensaje);
        }
    }
}

