using BackForoP.Data;

var builder = WebApplication.CreateBuilder(args);

//Configuaraci�n de las Cors
var RCors = "ReglasCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: RCors,
                      builder =>
                      {
                          builder.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

// Agregar servicios a contenedor
builder.Services.AddScoped<ConexionBD>(); // Agregar ConexionBD como un servicio de �mbito
builder.Services.AddScoped<UsuarioD>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

//Agregar cords
app.UseCors(RCors);

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
