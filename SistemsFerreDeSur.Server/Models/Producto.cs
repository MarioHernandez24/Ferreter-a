using System;
using System.Collections.Generic;

namespace SistemsFerreDeSur.Server.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? PrecioVenta { get; set; }
        public string? Caracteristicas { get; set; }
        public bool Estado { get; set; }
        public string? Detalle { get; set; }
        public int? IdUnidad { get; set; }
        public int? IdCategoria { get; set; }
        public int? Existencia { get; set; }

        // Navegación a otras entidades
        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
        public virtual UnidadMedida IdUnidadNavigation { get; set; } = null!;

        // Colecciones de otras entidades relacionadas
        public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
        public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
        public virtual ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}

