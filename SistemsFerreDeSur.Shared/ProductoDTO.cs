using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemsFerreDeSur.Shared
{
   
    public class ProductoDTO
    {
        /// <summary>
        /// Identificador único del producto.
        /// </summary>
        public int IdProducto { get; set; }

        /// <summary>
        /// Nombre del producto.
        /// </summary>
        //[Required]
        //[StringLength(100, ErrorMessage = "El nombre del producto no puede exceder los 100 caracteres.")]
        public string NombreProducto { get; set; } = null!;

        /// <summary>
        /// Cantidad de producto disponible.
        /// </summary>
        //[Range(0, int.MaxValue, ErrorMessage = "La cantidad no puede ser negativa.")]
        public int Cantidad { get; set; }
        /// <summary>
        /// Precio de compra del producto.
        /// </summary>
        //[Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "El precio de compra debe ser mayor que cero.")]
        public decimal PrecioCompra { get; set; }

        /// <summary>
        /// Precio de venta del producto.
        /// </summary>
        //[Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "El precio de venta debe ser mayor que cero.")]
        public decimal PrecioVenta { get; set; }


        /// <summary>
        /// Características del producto.
        /// </summary>
        //[Required]
        //[StringLength(500, ErrorMessage = "Las características no pueden exceder los 500 caracteres.")]
        public string Caracteristicas { get; set; } = null!;

        /// <summary>
        /// Estado del producto (activo o inactivo).
        /// </summary>
        public bool Estado { get; set; } // true para Activo, false para Inactivo

        /// <summary>
        /// Detalle adicional del producto.
        /// </summary>
       // [Required]
        //[StringLength(500, ErrorMessage = "El detalle no puede exceder los 500 caracteres.")]
        public string Detalle { get; set; } = null!;

        /// <summary>
        /// Identificador de la unidad de medida del producto.
        /// </summary>
        //[Range(1, int.MaxValue, ErrorMessage = "El IdUnidad debe ser mayor que cero.")]
        public int IdUnidad { get; set; }

        /// <summary>
        /// Identificador de la categoría del producto.
        /// </summary>
        //[Range(1, int.MaxValue, ErrorMessage = "El IdCategoria debe ser mayor que cero.")]
        public int IdCategoria { get; set; }

        /// <summary>
        /// Existencia actual del producto en inventario.
        /// </summary>
     // [Range(0, int.MaxValue, ErrorMessage = "La existencia no puede ser negativa.")]
        public int Existencia { get; set; }
    }
}
