using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemsFerreDeSur.Server.Models;
using SistemsFerreDeSur.Shared;

namespace SistemsFerreDeSur.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidaController : ControllerBase
    {
        private readonly FerreteriaDContext _dbContext;

        public UnidadMedidaController(FerreteriaDContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/UnidadMedida/Lista
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<UnidadMedidaDTO>>();
            var listaUnidadMedidaDTO = new List<UnidadMedidaDTO>();

            try
            {
                foreach (var item in await _dbContext.UnidadMedida.ToListAsync())
                {
                    listaUnidadMedidaDTO.Add(new UnidadMedidaDTO
                    {
                        IdUnidad = item.IdUnidad,
                        NombreUnidad = item.NombreUnidad,
                        Simbolo = item.Simbolo,
                        Descripcion = item.Descripcion,
                        TipoUnidad = item.TipoUnidad
                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaUnidadMedidaDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        // GET: api/UnidadMedida/Buscar/{id}
        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<UnidadMedidaDTO>();

            try
            {
                var dbUnidadMedida = await _dbContext.UnidadMedida.FirstOrDefaultAsync(x => x.IdUnidad == id);

                if (dbUnidadMedida != null)
                {
                    var unidadMedidaDTO = new UnidadMedidaDTO
                    {
                        IdUnidad = dbUnidadMedida.IdUnidad,
                        NombreUnidad = dbUnidadMedida.NombreUnidad,
                        Simbolo = dbUnidadMedida.Simbolo,
                        Descripcion = dbUnidadMedida.Descripcion,
                        TipoUnidad = dbUnidadMedida.TipoUnidad
                    };

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = unidadMedidaDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Unidad de medida no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        // POST: api/UnidadMedida/Guardar
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(UnidadMedidaDTO unidadMedida)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUnidadMedida = new UnidadMedida
                {
                    NombreUnidad = unidadMedida.NombreUnidad,
                    Simbolo = unidadMedida.Simbolo,
                    Descripcion = unidadMedida.Descripcion,
                    TipoUnidad = unidadMedida.TipoUnidad
                };

                _dbContext.UnidadMedida.Add(dbUnidadMedida);
                await _dbContext.SaveChangesAsync();

                responseApi.EsCorrecto = true;
                responseApi.Valor = dbUnidadMedida.IdUnidad;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        // PUT: api/UnidadMedida/Editar/{id}
        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(UnidadMedidaDTO unidadMedida, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUnidadMedida = await _dbContext.UnidadMedida.FirstOrDefaultAsync(e => e.IdUnidad == id);

                if (dbUnidadMedida != null)
                {
                    dbUnidadMedida.NombreUnidad = unidadMedida.NombreUnidad;
                    dbUnidadMedida.Simbolo = unidadMedida.Simbolo;
                    dbUnidadMedida.Descripcion = unidadMedida.Descripcion;
                    dbUnidadMedida.TipoUnidad = unidadMedida.TipoUnidad;

                    _dbContext.UnidadMedida.Update(dbUnidadMedida);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbUnidadMedida.IdUnidad;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Unidad de medida no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        // DELETE: api/UnidadMedida/Eliminar/{id}
        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbUnidadMedida = await _dbContext.UnidadMedida.FirstOrDefaultAsync(e => e.IdUnidad == id);

                if (dbUnidadMedida != null)
                {
                    _dbContext.UnidadMedida.Remove(dbUnidadMedida);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Unidad de medida no encontrada";
                }
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
