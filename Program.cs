using ferreteriaJuanito;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SqliteDBContext>(p => p.UseSqlite(builder.Configuration.GetConnectionString("CadenaDeConexion")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IProductosService, ProductosService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run("http://localhost:4000");
