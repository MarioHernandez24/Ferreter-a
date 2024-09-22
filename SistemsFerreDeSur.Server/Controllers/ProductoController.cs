using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemsFerreDeSur.Server.Models;
using SistemsFerreDeSur.Shared; // Asegúrate de que los DTOs están aquí
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemsFerreDeSur.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly FerreteriaDContext _dbContext;

        public ProductoController(FerreteriaDContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<ProductoDTO>>();

            try
            {
                var listaProductoDTO = await _dbContext.Productos
                    .Include(p => p.IdCategoriaNavigation) // Incluye Categoria
                    .Include(p => p.IdUnidadNavigation)    // Incluye UnidadMedida
                    .Select(item => new ProductoDTO
                    {
                        IdProducto = item.IdProducto,
                        NombreProducto = item.NombreProducto,
                        Cantidad = item.Cantidad,
                        PrecioCompra = item.PrecioCompra,
                        PrecioVenta = item.PrecioVenta,
                        Caracteristicas = item.Caracteristicas,
                        Estado = item.Estado,
                        Detalle = item.Detalle,
                        IdUnidad = item.IdUnidad,
                        IdCategoria = item.IdCategoria,
                        Existencia = item.Existencia
                    }).ToListAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaProductoDTO;
                return Ok(responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<ProductoDTO>();

            try
            {
                var dbProducto = await _dbContext.Productos
                    .Include(p => p.IdCategoriaNavigation) // Incluye Categoria
                    .Include(p => p.IdUnidadNavigation)    // Incluye UnidadMedida
                    .FirstOrDefaultAsync(x => x.IdProducto == id);

                if (dbProducto != null)
                {
                    var productoDTO = new ProductoDTO
                    {
                        IdProducto = dbProducto.IdProducto,
                        NombreProducto = dbProducto.NombreProducto,
                        Cantidad = dbProducto.Cantidad,
                        PrecioCompra = dbProducto.PrecioCompra,
                        PrecioVenta = dbProducto.PrecioVenta,
                        Caracteristicas = dbProducto.Caracteristicas,
                        Estado = dbProducto.Estado,
                        Detalle = dbProducto.Detalle,
                        IdUnidad = dbProducto.IdUnidad,
                        IdCategoria = dbProducto.IdCategoria,
                        Existencia = dbProducto.Existencia
                    };

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = productoDTO;
                    return Ok(responseApi);
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                    return NotFound(responseApi);
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(ProductoDTO productoDto)
        {
            var responseApi = new ResponseAPI<int>();

            if (!ModelState.IsValid)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Datos del producto no válidos";
                return BadRequest(responseApi);
            }

            try
            {
                var dbProducto = new Producto
                {
                    NombreProducto = productoDto.NombreProducto,
                    Cantidad = productoDto.Cantidad,
                    PrecioCompra = productoDto.PrecioCompra,
                    PrecioVenta = productoDto.PrecioVenta,
                    Caracteristicas = productoDto.Caracteristicas,
                    Estado = productoDto.Estado,
                    Detalle = productoDto.Detalle,
                    IdUnidad = productoDto.IdUnidad,
                    IdCategoria = productoDto.IdCategoria,
                    Existencia = productoDto.Existencia
                };

                _dbContext.Productos.Add(dbProducto);
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbProducto.IdProducto;
                return CreatedAtAction(nameof(Buscar), new { id = dbProducto.IdProducto }, responseApi);
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar([FromBody] ProductoDTO productoDto, int id)
        {
            var responseApi = new ResponseAPI<int>();

            if (!ModelState.IsValid)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = "Datos del producto no válidos";
                return BadRequest(responseApi);
            }

            try
            {
                var dbProducto = await _dbContext.Productos.FirstOrDefaultAsync(e => e.IdProducto == id);

                if (dbProducto != null)
                {
                    dbProducto.NombreProducto = productoDto.NombreProducto;
                    dbProducto.Cantidad = productoDto.Cantidad;
                    dbProducto.PrecioCompra = productoDto.PrecioCompra;
                    dbProducto.PrecioVenta = productoDto.PrecioVenta;
                    dbProducto.Caracteristicas = productoDto.Caracteristicas;
                    dbProducto.Estado = productoDto.Estado;
                    dbProducto.Detalle = productoDto.Detalle;
                    dbProducto.IdUnidad = productoDto.IdUnidad;
                    dbProducto.IdCategoria = productoDto.IdCategoria;
                    dbProducto.Existencia = productoDto.Existencia;

                    _dbContext.Productos.Update(dbProducto);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProducto.IdProducto;
                    return Ok(responseApi);
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                    return NotFound(responseApi);
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProducto = await _dbContext.Productos.FirstOrDefaultAsync(e => e.IdProducto == id);

                if (dbProducto != null)
                {
                    _dbContext.Productos.Remove(dbProducto);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    return Ok(responseApi);
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Producto no encontrado";
                    return NotFound(responseApi);
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseApi);
            }
        }
    }
}
