using Microsoft.EntityFrameworkCore;

namespace ferreteriaJuanito;

public class SqliteDBContext : DbContext
{

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Carrito> Carritos { get; set; }

    public SqliteDBContext(DbContextOptions<SqliteDBContext> options) : base(options) { }

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
            carrito.HasKey(p=> p.CarritoId);
            //carrito.HasOne(p => p.Usuarios).WithMany(p => p.Carritos).HasForeignKey(p => p.UsuarioId);
            carrito.Property(p => p.UsuarioId).IsRequired();
            carrito.HasOne(p => p.Productos).WithMany(p => p.Carritos).HasForeignKey(p => p.ProductoId);
            carrito.Property(p => p.Cantidad).IsRequired();
        });
    }
}