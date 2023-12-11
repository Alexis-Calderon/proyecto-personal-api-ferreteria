using System.Text;
using ferreteriaJuanito;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

// Se inyectan las dependencias necesarias para funcionar correctamente. 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SqliteDBContext>(p => p.UseSqlite(builder.Configuration.GetConnectionString("CadenaDeConexion")));
builder.Services.AddControllers();

// Se configuran las opciones de autenticacion con el jwt.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IProductosService, ProductosService>();
builder.Services.AddScoped<ICarritosService, CarritosServise>();
builder.Services.AddScoped<IVentasService, VentasService>();
builder.Services.AddScoped<ILoginService, LoginService>();
var app = builder.Build();

// Se agrega un middleware personalizado encargado de las excepciones globales.
app.UseMiddleware<GlobalExceptionsService>();

// Se valida que el ambiente sea de desarrollo para poder hacer uso de la libreria Swagger.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Se inicia la api escuchando por el puerto 4000.
app.Run("http://localhost:4000");