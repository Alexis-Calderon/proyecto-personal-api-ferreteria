using ferreteriaJuanito;
using Microsoft.EntityFrameworkCore;

// Se inyectan las dependencias necesarias para funcionar correctamente. 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SqliteDBContext>(p => p.UseSqlite(builder.Configuration.GetConnectionString("CadenaDeConexion")));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IProductosService, ProductosService>();
builder.Services.AddScoped<ICarritosService, CarritosServise>();
builder.Services.AddScoped<IVentasService, VentasService>();
var app = builder.Build();

// Se agrega un middleware personalizado encargado de las excepciones globales.
app.UseMiddleware<GlobalExceptionsService>();

// Se valida que el ambiente sea de desarrollo para poder hacer uso de la libreria Swagger.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Se mapean los controladores de la api
app.MapControllers();

// Se inicia la api escuchando por el puerto 4000.
app.Run("http://localhost:4000");