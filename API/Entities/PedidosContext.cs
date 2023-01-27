using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Entities;

public partial class PedidosContext : DbContext
{
    public PedidosContext()
    {
    }

    public PedidosContext(DbContextOptions<PedidosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cabecera> Cabeceras { get; set; }

    public virtual DbSet<Detalle> Detalles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cabecera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cabecera");

            entity.Property(e => e.Direccion).HasMaxLength(30);
            entity.Property(e => e.Estado).HasMaxLength(30);
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.Temperatura).HasPrecision(10);
            entity.Property(e => e.Total).HasPrecision(10);
            entity.Property(e => e.Usuario).HasMaxLength(30);
        });

        modelBuilder.Entity<Detalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("detalle");

            entity.HasIndex(e => e.IdCabecera, "IdCabecera");

            entity.Property(e => e.Cantidad).HasPrecision(30);
            entity.Property(e => e.Precio).HasPrecision(30);
            entity.Property(e => e.Producto).HasMaxLength(30);

            entity.HasOne(d => d.IdCabeceraNavigation).WithMany(p => p.Detalles)
                .HasForeignKey(d => d.IdCabecera)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("detalle_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
