using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemsFerreDeSur.Server.Models;
using SistemsFerreDeSur.Shared;
using Microsoft.EntityFrameworkCore; 

namespace SistemsFerreDeSur.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly FerreteriaDContext _dbContext;

        public ProveedorController(FerreteriaDContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<ProveedorDTO>>();
            var listaProveedorDTO = new List<ProveedorDTO>();

            try 
            {
                foreach (var item in await _dbContext.Proveedor.ToListAsync())
                {
                    listaProveedorDTO.Add(new ProveedorDTO
                    {
                        IdProveedor = item.IdProveedor,
                        NombreProveedor = item.NombreProveedor,
                        DireccionEmpresa = item.DireccionEmpresa,
                        Telefono = item.Telefono,
                        RazonSocial = item.RazonSocial,
                        NumeroRuc = item.NumeroRuc,
                    });

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = listaProveedorDTO;
                }
            }

            catch(Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<ProveedorDTO>();
            var ProveedorDTO = new ProveedorDTO();

            try
            {
                var dbProveedor = await _dbContext.Proveedor.FirstOrDefaultAsync(x => x.IdProveedor == id);

                if(dbProveedor != null) 
                {
                    ProveedorDTO.IdProveedor = dbProveedor.IdProveedor;
                    ProveedorDTO.NombreProveedor = dbProveedor.NombreProveedor;
                    ProveedorDTO.DireccionEmpresa = dbProveedor.DireccionEmpresa;
                    ProveedorDTO.Telefono = dbProveedor.Telefono;
                    ProveedorDTO.RazonSocial = dbProveedor.RazonSocial;
                    ProveedorDTO.NumeroRuc = dbProveedor.NumeroRuc;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = ProveedorDTO;   
                }
                else
                {   
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }

            }

            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(ProveedorDTO proveedor)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProveedor = new Proveedor
                {
                    NombreProveedor = proveedor.NombreProveedor,
                    DireccionEmpresa = proveedor.DireccionEmpresa,
                    Telefono = proveedor.Telefono,
                    RazonSocial = proveedor.RazonSocial,
                    NumeroRuc = proveedor.NumeroRuc,
                };

                _dbContext.Proveedor.Add(dbProveedor);
                await _dbContext.SaveChangesAsync();

                if(dbProveedor.IdProveedor != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProveedor.IdProveedor;
                }
                else
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "No guardado";
                }
             
               
            }

            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }



        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(ProveedorDTO proveedor, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbProveedor = await _dbContext.Proveedor.FirstOrDefaultAsync(e => e.IdProveedor == id);

                

                if (dbProveedor != null)
                {
                    dbProveedor.NombreProveedor = proveedor.NombreProveedor;
                    dbProveedor.DireccionEmpresa = proveedor.DireccionEmpresa;
                    dbProveedor.Telefono = proveedor.Telefono;
                    dbProveedor.RazonSocial = proveedor.RazonSocial;
                    dbProveedor.NumeroRuc = proveedor.NumeroRuc;    

                    _dbContext.Proveedor.Update(dbProveedor);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbProveedor.IdProveedor;

                }
                else
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Proveedor no encontrado";
                }


            }

            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
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
                var dbProveedor = await _dbContext.Proveedor.FirstOrDefaultAsync(e => e.IdProveedor == id);



                if (dbProveedor != null)
                {
                   
                    _dbContext.Proveedor.Remove(dbProveedor);
                    await _dbContext.SaveChangesAsync();

                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Mensaje = "Proveedor no encontrado";
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
