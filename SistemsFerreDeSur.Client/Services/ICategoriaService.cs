using SistemsFerreDeSur.Shared;

namespace SistemsFerreDeSur.Client.Services
{
    public interface ICategoriaService
    {
        // Obtiene una lista de categorías
        Task<List<CategoriaDTO>> Lista();

        // Busca una categoría por su ID
        Task<CategoriaDTO> Buscar(int id);

        // Guarda una nueva categoría o actualiza una existente
        Task<int> Guardar(CategoriaDTO categoria);

        // Edita una categoría existente
        Task<int> Editar(CategoriaDTO categoria);

        // Elimina una categoría por su ID
        Task<bool> Eliminar(int id);
    }
}
