using System;
using System.Collections.Generic;

namespace SistemsFerreDeSur.Server.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public bool Estado { get; set; } // Cambiado a bool

    public virtual ICollection<Inventario> Inventarios { get; } = new List<Inventario>();

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
