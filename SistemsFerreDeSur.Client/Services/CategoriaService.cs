using SistemsFerreDeSur.Shared;
using System.Net.Http.Json;

namespace SistemsFerreDeSur.Client.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _http;

        public CategoriaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CategoriaDTO>> Lista()
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ResponseAPI<List<CategoriaDTO>>>("api/Categoria/Lista");

                if (result?.EsCorrecto == true)
                {
                    return result.Valor ?? new List<CategoriaDTO>(); // Manejar posibles valores nulos
                }
                else
                {
                    throw new Exception(result?.Mensaje ?? "Error desconocido");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores adicionales, si es necesario
                throw new Exception($"Error al obtener la lista de categorías: {ex.Message}", ex);
            }
        }

        public async Task<CategoriaDTO> Buscar(int id)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ResponseAPI<CategoriaDTO>>($"api/Categoria/Buscar/{id}");

                if (result?.EsCorrecto == true)
                {
                    return result.Valor ?? throw new Exception("Categoría no encontrada"); // Manejar posibles valores nulos
                }
                else
                {
                    throw new Exception(result?.Mensaje ?? "Error desconocido");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores adicionales, si es necesario
                throw new Exception($"Error al buscar la categoría con ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<int> Guardar(CategoriaDTO categoria)
        {
            try
            {
                var result = await _http.PostAsJsonAsync("api/Categoria/Guardar", categoria);
                var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

                if (response?.EsCorrecto == true)
                {
                    return response.Valor;
                }
                else
                {
                    throw new Exception(response?.Mensaje ?? "Error desconocido");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores adicionales, si es necesario
                throw new Exception($"Error al guardar la categoría: {ex.Message}", ex);
            }
        }

        public async Task<int> Editar(CategoriaDTO categoria)
        {
            try
            {
                var result = await _http.PutAsJsonAsync($"api/Categoria/Editar/{categoria.IdCategoria}", categoria);
                var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

                if (response?.EsCorrecto == true)
                {
                    return response.Valor;
                }
                else
                {
                    throw new Exception(response?.Mensaje ?? "Error desconocido");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores adicionales, si es necesario
                throw new Exception($"Error al editar la categoría con ID {categoria.IdCategoria}: {ex.Message}", ex);
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var result = await _http.DeleteAsync($"api/Categoria/Eliminar/{id}");
                var response = await result.Content.ReadFromJsonAsync<ResponseAPI<int>>();

                if (response?.EsCorrecto == true)
                {
                    return true;
                }
                else
                {
                    throw new Exception(response?.Mensaje ?? "Error desconocido");
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores adicionales, si es necesario
                throw new Exception($"Error al eliminar la categoría con ID {id}: {ex.Message}", ex);
            }
        }
    }
}
