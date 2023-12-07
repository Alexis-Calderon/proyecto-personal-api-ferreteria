
using Microsoft.EntityFrameworkCore;

namespace ferreteriaJuanito;

public class SqliteDBContext : DbContext
{

    public DbSet<Usuario> Usuarios { get; set; }

    public SqliteDBContext(DbContextOptions<SqliteDBContext> options) : base(options)
    {

    }
}