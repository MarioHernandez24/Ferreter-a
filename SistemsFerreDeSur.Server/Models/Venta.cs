using System;
using System.Collections.Generic;

namespace SistemsFerreDeSur.Server.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public DateTime FechaVenta { get; set; }

    public string? DetallesVenta { get; set; }

    public string? Categoria { get; set; }

    public int? Existencias { get; set; }

    public double? PrecioVenta { get; set; }

    public int? Cantidad { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdProducto { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
