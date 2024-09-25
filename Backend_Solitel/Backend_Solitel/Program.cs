using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Conexión a BD
builder.Services.AddDbContext<AdmTareaContext>(options =>
{
    // Usar la cadena de conexión desde la configuración
    var connectionString = "Server=DESKTOP-I46ULUJ\\SQLEXPRESS;Database=AdmTareaBD;Trusted_Connection=True;TrustServerCertificate=True;";
    options.UseSqlServer(connectionString);
    // Otros ajustes del contexto de base de datos pueden ser configurados aquí, si es necesario
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
