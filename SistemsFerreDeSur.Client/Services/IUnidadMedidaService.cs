using SistemsFerreDeSur.Shared;

namespace SistemsFerreDeSur.Client.Services
{
    public interface IUnidadMedidaService
    {
        // Método para obtener una lista de todas las unidades de medida
        Task<List<UnidadMedidaDTO>> Lista();

        // Método para buscar una unidad de medida por su ID
        Task<UnidadMedidaDTO> Buscar(int id);

        // Método para guardar una nueva unidad de medida
        Task<int> Guardar(UnidadMedidaDTO unidadMedida);

        // Método para editar una unidad de medida existente
        Task<int> Editar(UnidadMedidaDTO unidadMedida);

        // Método para eliminar una unidad de medida por su ID
        Task<bool> Eliminar(int id);
    }
}
