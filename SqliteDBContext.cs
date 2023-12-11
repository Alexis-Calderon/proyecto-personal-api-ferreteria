using Microsoft.EntityFrameworkCore;

namespace ferreteriaJuanito;

// Aqui se contruye el contexto de datos que se va a utilizar en la API
public class SqliteDBContext : DbContext
{

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<VentaDetalle> VentaDetalles { get; set; }

    public SqliteDBContext(DbContextOptions<SqliteDBContext> options) : base(options) { }


    // Se configuran las propiedades y las entidades del modelo de datos usando FluenT API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Usuario>(usuario =>
        {
            usuario.ToTable("Usuario");
            usuario.HasKey(p => p.UsuarioId);
            usuario.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            usuario.Property(p => p.Correo).IsRequired().HasMaxLength(100);
            usuario.Property(p => p.Contraseña).IsRequired();
            usuario.Property(p => p.Rol).IsRequired();
        });

        modelBuilder.Entity<Producto>(producto =>
        {
            producto.ToTable("Producto");
            producto.HasKey(p => p.ProductoId);
            producto.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            producto.Property(p => p.Precio).IsRequired();
            producto.Property(p => p.UnidadesDeMedida).IsRequired();
            producto.Property(p => p.Stock).IsRequired();
        });

        modelBuilder.Entity<Carrito>(carrito =>
        {
            carrito.ToTable("Carrito");
            carrito.HasKey(p => p.CarritoId);
            carrito.Property(p => p.UsuarioId).IsRequired();
            carrito.HasOne(p => p.Producto).WithMany(p => p.Carritos).HasForeignKey(p => p.ProductoId);
            carrito.Property(p => p.Cantidad).IsRequired();
        });

        modelBuilder.Entity<VentaDetalle>(ventaDetalle =>
        {
            ventaDetalle.ToTable("VentasDetalle");
            ventaDetalle.HasKey(p => p.VentaDetalleId);
            ventaDetalle.HasOne(p => p.Venta).WithMany(p => p.ventaDetalles).HasForeignKey(p => p.VentaId);
            ventaDetalle.HasOne(p => p.Producto).WithMany(p => p.ventaDetalles).HasForeignKey(p => p.ProductoId);
            ventaDetalle.Property(p => p.Cantidad).IsRequired();
            ventaDetalle.Property(p => p.Precio).IsRequired();
        });

        modelBuilder.Entity<Venta>(venta =>
        {
            venta.ToTable("Ventas");
            venta.HasKey(p => p.VentaId);
            venta.HasOne(p => p.Usuario).WithMany(p => p.Ventas).HasForeignKey(p => p.UsuarioId);
            venta.Property(p => p.Fecha).IsRequired();
        });
    }
}