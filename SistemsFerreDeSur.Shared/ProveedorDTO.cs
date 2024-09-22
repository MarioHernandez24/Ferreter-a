using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace SistemsFerreDeSur.Shared
{
    public class ProveedorDTO
    {
        public int IdProveedor { get; set; }

        [Required (ErrorMessage ="El campo{0} es requerido.")]
        public string NombreProveedor { get; set; } = null!;

        [Required(ErrorMessage = "El campo{0} es requerido.")]
        public string? DireccionEmpresa { get; set; }

        [Required(ErrorMessage = "El campo{0} es requerido.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El campo{0} es requerido.")]
        public string? RazonSocial { get; set; }

        [Required(ErrorMessage = "El campo{0} es requerido.")]
        public string? NumeroRuc { get; set; }

    }
}
