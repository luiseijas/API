using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Detalle
{
    public int Id { get; set; }

    public int? IdCabecera { get; set; }

    public string? Producto { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Cantidad { get; set; }

    public virtual Cabecera? IdCabeceraNavigation { get; set; }
}
