using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SistemsFerreDeSur.Shared;

namespace SistemsFerreDeSur.Client.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;

        public ProductoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtiene la lista de productos desde la API.
        /// </summary>
        public async Task<List<ProductoDTO>> Lista()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<ProductoDTO>>("api/producto/lista");
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception("Error al obtener la lista de productos.", ex);
            }
        }

        /// <summary>
        /// Busca un producto por su ID.
        /// </summary>
        public async Task<ProductoDTO> Buscar(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProductoDTO>($"api/producto/buscar/{id}");
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception($"Error al buscar el producto con ID {id}.", ex);
            }
        }

        /// <summary>
        /// Guarda un nuevo producto.
        /// </summary>
        public async Task<int> Guardar(ProductoDTO producto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/producto/guardar", producto);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    // Manejar la respuesta no exitosa
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al guardar el producto: {error}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception("Error al guardar el producto.", ex);
            }
        }

        /// <summary>
        /// Edita un producto existente.
        /// </summary>
        public async Task<int> Editar(ProductoDTO producto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/producto/editar/{producto.IdProducto}", producto);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<int>();
                }
                else
                {
                    // Manejar la respuesta no exitosa
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Error al editar el producto: {error}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception("Error al editar el producto.", ex);
            }
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/producto/eliminar/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                // Manejar excepción y registrar error
                // Aquí podrías loggear el error o mostrar un mensaje de usuario
                throw new Exception($"Error al eliminar el producto con ID {id}.", ex);
            }
        }
    }
}
