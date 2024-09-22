using System;
using System.Collections.Generic;

namespace SistemsFerreDeSur.Server.Models;

public partial class Reporte
{
    public int IdReporte { get; set; }

    public string TipoReporte { get; set; } = null!;

    public string Periodo { get; set; } = null!;

    public string DetallesReporte { get; set; } = null!;
}
