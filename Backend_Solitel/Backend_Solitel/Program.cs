using BW.CU;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using DA.Acciones;
using DA.Contexto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IGestionarFiscaliaBW, GestionarFiscaliaBW>();
builder.Services.AddTransient<IGestionarFiscaliaDA, GestionarFiscaliaDA>();


//Conexión a BD
builder.Services.AddDbContext<SolitelContext>(options =>
{
    // Usar la cadena de conexión desde la configuración
    var connectionString = "Server=tcp:163.178.107.10;User Id=laboratorios;Password=_)Ui7%-cX!?xw=t\"$;Initial Catalog=Solitel_Database;TrustServerCertificate=true;";
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
