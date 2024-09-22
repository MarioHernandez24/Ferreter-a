using System;
using System.Collections.Generic;

namespace SistemsFerreDeSur.Server.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public string Categoria { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Estado { get; set; } = null!;

    public string? Detalle { get; set; }

    public string? Codigo { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdProducto { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
