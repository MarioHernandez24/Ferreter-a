using System;
using System.Collections.Generic;

namespace SistemsFerreDeSur.Server.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public DateTime FechaCompra { get; set; }

    public int IdProveedor { get; set; }

    public string? DetallesCompra { get; set; }

    public string? FormaPago { get; set; }

    public int? CantidadComprada { get; set; }

    public double? PrecioCompra { get; set; }

    public int? IdProducto { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Proveedor IdProveedorNavigation { get; set; } = null!;
}
