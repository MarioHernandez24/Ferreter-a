using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemsFerreDeSur.Server.Models;

public partial class FerreteriaDContext : DbContext
{
    public FerreteriaDContext()
    {
    }

    public FerreteriaDContext(DbContextOptions<FerreteriaDContext> options)
        : base(options)
    {
    }
    

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<UnidadMedida> UnidadMedida { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }


    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categori__8A3D240CA60C753E");

            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('Activo')")
                .HasColumnName("estado");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreCategoria");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compra__48B99DB7DA6E4A51");

            entity.ToTable("Compra");

            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.CantidadComprada).HasColumnName("cantidadComprada");
            entity.Property(e => e.DetallesCompra)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detallesCompra");
            entity.Property(e => e.FechaCompra)
                .HasColumnType("date")
                .HasColumnName("fechaCompra");
            entity.Property(e => e.FormaPago)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("formaPago");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.PrecioCompra).HasColumnName("precioCompra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Compra__idProduc__5070F446");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compra__idProvee__4F7CD00D");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.IdInventario).HasName("PK__Inventar__8F145B0D36277AD8");

            entity.ToTable("Inventario");

            entity.Property(e => e.IdInventario).HasColumnName("idInventario");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Categoria)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("codigo");
            entity.Property(e => e.Detalle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Inventari__idCat__5629CD9C");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Inventari__idPro__571DF1D5");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A132D100E534");

            entity.ToTable("Producto");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProducto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.PrecioCompra).HasColumnName("precioCompra");
            entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta");
            entity.Property(e => e.Caracteristicas)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("caracteristicas");
            entity.Property(e => e.Estado)
                .HasColumnName("estado");  // Cambiado a tipo bool en el modelo
            entity.Property(e => e.Detalle)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("detalle");
            entity.Property(e => e.Existencia).HasColumnName("existencia");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");

            // Relaciones
            entity.HasOne(d => d.IdCategoriaNavigation)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__idCate__5535A963");

            entity.HasOne(d => d.IdUnidadNavigation)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdUnidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__idUnid__44FF419A");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__A3FA8E6BDB6E9DDC");

            entity.ToTable("Proveedor");

            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.DireccionEmpresa)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccionEmpresa");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreProveedor");
            entity.Property(e => e.NumeroRuc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numeroRUC");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.IdReporte).HasName("PK__Reporte__40D65D3CEFDE8DC1");

            entity.ToTable("Reporte");

            entity.Property(e => e.IdReporte).HasColumnName("idReporte");
            entity.Property(e => e.DetallesReporte)
                .HasColumnType("text")
                .HasColumnName("detallesReporte");
            entity.Property(e => e.Periodo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("periodo");
            entity.Property(e => e.TipoReporte)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoReporte");
        });

        modelBuilder.Entity<UnidadMedida>(entity =>
        {
            entity.HasKey(e => e.IdUnidad).HasName("PK__UnidadMe__34C1E8D7F0E055CB");

            entity.Property(e => e.IdUnidad).HasColumnName("idUnidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreUnidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombreUnidad");
            entity.Property(e => e.Simbolo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("simbolo");
            entity.Property(e => e.TipoUnidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipoUnidad");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__645723A6245CE30D");

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.EsActivo).HasColumnName("esActivo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NombreApellidos)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreApellidos");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Venta__077D561488A81402");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.DetallesVenta)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("detallesVenta");
            entity.Property(e => e.Existencias).HasColumnName("existencias");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("date")
                .HasColumnName("fechaVenta");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Ventas)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Venta__idProduct__4CA06362");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
