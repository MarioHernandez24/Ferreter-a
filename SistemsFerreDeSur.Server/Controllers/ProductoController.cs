using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemsFerreDeSur.Server.Models;
using SistemsFerreDeSur.Shared; // Asegúrate de que los DTOs están aquí
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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
    .Include(p => p.IdCategoriaNavigation)
    .Include(p => p.IdUnidadNavigation)
    .Select(item => new ProductoDTO
    {
        IdProducto = item.IdProducto,
        NombreProducto = item.NombreProducto != null ? item.NombreProducto : "Producto desconocido",
        Cantidad = item.Cantidad.HasValue ? item.Cantidad.Value : 0,
        PrecioCompra = item.PrecioCompra.HasValue ? item.PrecioCompra.Value : 0.00m,
        PrecioVenta = item.PrecioVenta.HasValue ? item.PrecioVenta.Value : 0.00m,
        Caracteristicas = item.Caracteristicas != null ? item.Caracteristicas : "Sin características",
        Estado = item.Estado,
        Detalle = item.Detalle != null ? item.Detalle : "Sin detalles",
        IdUnidad = item.IdUnidad.HasValue ? item.IdUnidad.Value : 1,
        IdCategoria = item.IdCategoria.HasValue ? item.IdCategoria.Value : 4,
        Existencia = item.Existencia.HasValue ? item.Existencia.Value : 0
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
                        NombreProducto = dbProducto.NombreProducto != null ? dbProducto.NombreProducto : "Producto desconocido",
                        Cantidad = dbProducto.Cantidad.HasValue ? dbProducto.Cantidad.Value : 0,
                        PrecioCompra = dbProducto.PrecioCompra.HasValue ? dbProducto.PrecioCompra.Value : 0.00m,
                        PrecioVenta = dbProducto.PrecioVenta.HasValue ? dbProducto.PrecioVenta.Value : 0.00m,
                        Caracteristicas = dbProducto.Caracteristicas != null ? dbProducto.Caracteristicas : "Sin características",
                        Estado = dbProducto.Estado,
                        Detalle = dbProducto.Detalle != null ? dbProducto.Detalle : "Sin detalles",
                        IdUnidad = dbProducto.IdUnidad.HasValue ? dbProducto.IdUnidad.Value : 1,
                        IdCategoria = dbProducto.IdCategoria.HasValue ? dbProducto.IdCategoria.Value : 4,
                        Existencia = dbProducto.Existencia.HasValue ? dbProducto.Existencia.Value : 0
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
