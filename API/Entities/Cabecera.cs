using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Cabecera
{
    public int Id { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public string? Estado { get; set; }

    public string? Usuario { get; set; }

    public string? Direccion { get; set; }

    public double? Humedad { get; set; }

    public decimal? Temperatura { get; set; }

    public virtual ICollection<Detalle> Detalles { get; } = new List<Detalle>();
}
