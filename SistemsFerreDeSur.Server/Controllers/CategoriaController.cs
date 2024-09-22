using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemsFerreDeSur.Server.Models;
using SistemsFerreDeSur.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemsFerreDeSur.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly FerreteriaDContext _dbContext;

        public CategoriaController(FerreteriaDContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Categoria/Lista
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<CategoriaDTO>>();

            try
            {
                var categorias = await _dbContext.Categoria.ToListAsync();
                var listaCategoriaDTO = categorias.Select(item => new CategoriaDTO
                {
                    IdCategoria = item.IdCategoria,
                    NombreCategoria = item.NombreCategoria,
                    Estado = item.Estado
                }).ToList();

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaCategoriaDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error al obtener la lista de categorías: {ex.Message}";
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<CategoriaDTO>();

            try
            {
                var dbCategoria = await _dbContext.Categoria.FirstOrDefaultAsync(x => x.IdCategoria == id);

                if (dbCategoria != null)
                {
                    var categoriaDTO = new CategoriaDTO
                    {
                        IdCategoria = dbCategoria.IdCategoria,
                        NombreCategoria = dbCategoria.NombreCategoria,
                        Estado = dbCategoria.Estado
                    };

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = categoriaDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Categoría no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error al buscar la categoría: {ex.Message}";
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(CategoriaDTO categoria)
        {
            var responseApi = new ResponseAPI<int>();

            // Validación del modelo
            if (!ModelState.IsValid)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Datos del modelo no válidos.";
                return BadRequest(responseApi);
            }

            try
            {
                // Mapear el DTO a la entidad de la base de datos
                var dbCategoria = new Categoria
                {
                    NombreCategoria = categoria.NombreCategoria,
                    Estado = categoria.Estado
                };

                // Agregar la categoría a la base de datos
                _dbContext.Categoria.Add(dbCategoria);
                await _dbContext.SaveChangesAsync();

                // Configurar la respuesta
                responseApi.EsCorrecto = true;
                responseApi.Valor = dbCategoria.IdCategoria; // Suponiendo que IdCategoria es el ID generado
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error al guardar la categoría: {ex.Message}";
            }

            // Retornar la respuesta
            return Ok(responseApi);
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar([FromBody] CategoriaDTO categoria, int id)
        {
            var responseApi = new ResponseAPI<int>();

            if (!ModelState.IsValid)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Datos del modelo no válidos.";
                return BadRequest(responseApi);
            }

            try
            {
                var dbCategoria = await _dbContext.Categoria.FirstOrDefaultAsync(e => e.IdCategoria == id);

                if (dbCategoria != null)
                {
                    dbCategoria.NombreCategoria = categoria.NombreCategoria;
                    dbCategoria.Estado = categoria.Estado;

                    _dbContext.Categoria.Update(dbCategoria);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbCategoria.IdCategoria;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Categoría no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error al editar la categoría: {ex.Message}";
            }

            return Ok(responseApi);
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbCategoria = await _dbContext.Categoria.FirstOrDefaultAsync(e => e.IdCategoria == id);

                if (dbCategoria != null)
                {
                    _dbContext.Categoria.Remove(dbCategoria);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Categoría no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = $"Error al eliminar la categoría: {ex.Message}";
            }

            return Ok(responseApi);
        }
    }
}

