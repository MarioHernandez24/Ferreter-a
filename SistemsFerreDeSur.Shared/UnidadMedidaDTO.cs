using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemsFerreDeSur.Shared
{
    public class UnidadMedidaDTO
    {
        public int IdUnidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string NombreUnidad { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Simbolo { get; set; } = null!;

        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string TipoUnidad { get; set; } = null!;
    }
}
