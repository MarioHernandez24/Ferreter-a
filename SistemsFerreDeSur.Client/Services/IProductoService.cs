using System.Collections.Generic;
using System.Threading.Tasks;
using SistemsFerreDeSur.Shared;  // Asegúrate de que esta directiva esté correcta para tus DTOs

namespace SistemsFerreDeSur.Client.Services
{
    /// <summary>
    /// Define los métodos para interactuar con los servicios de productos.
    /// </summary>
    public interface IProductoService
    {
        /// <summary>
        /// Obtiene una lista de productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        Task<List<ProductoDTO>> Lista();

        /// <summary>
        /// Busca un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto.</param>
        /// <returns>Detalles del producto.</returns>
        Task<ProductoDTO> Buscar(int id);

        /// <summary>
        /// Guarda un nuevo producto.
        /// </summary>
        /// <param name="producto">Datos del producto a guardar.</param>
        /// <returns>ID del producto guardado.</returns>
        Task<int> Guardar(ProductoDTO producto);

        /// <summary>
        /// Edita un producto existente.
        /// </summary>
        /// <param name="producto">Datos del producto a editar.</param>
        /// <returns>ID del producto editado.</returns>
        Task<int> Editar(ProductoDTO producto);

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>Indica si la eliminación fue exitosa.</returns>
        Task<bool> Eliminar(int id);
    }
}
