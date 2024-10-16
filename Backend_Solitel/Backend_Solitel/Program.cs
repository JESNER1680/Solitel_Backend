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

builder.Services.AddTransient<IGestionarDelitoBW, GestionarDelitoBW>();
builder.Services.AddTransient<IGestionarDelitoDA, GestionarDelitoDA>();

builder.Services.AddTransient<IGestionarCategoriaDelitoBW, GestionarCategoriaDelitoBW>();
builder.Services.AddTransient<IGestionarCategoriaDelitoDA, GestionarCategoriaDelitoDA>();

builder.Services.AddTransient<IGestionarCondicionBW, GestionarCondicionBW>();
builder.Services.AddTransient<IGestionarCondicionDA, GestionarCondicionDA>();

builder.Services.AddTransient<IGestionarModalidadBW, GestionarModalidadBW>();
builder.Services.AddTransient<IGestionarModalidadDA, GestionarModalidadDA>();

builder.Services.AddTransient<IGestionarSubModalidadBW, GestionarSubModalidadBW>();
builder.Services.AddTransient<IGestionarSubModalidadDA, GestionarSubModalidadDA>();

builder.Services.AddTransient<IGestionarTipoSolicitudBW, GestionarTipoSolicitudBW>();
builder.Services.AddTransient<IGestionarTipoSolicitudDA, GestionarTipoSolicitudDA>();

builder.Services.AddTransient<IGestionarTipoDatoBW, GestionarTipoDatoBW>();
builder.Services.AddTransient<IGestionarTipoDatoDA, GestionarTipoDatoDA>();

//Conexión a BD
builder.Services.AddDbContext<SolitelContext>(options =>
{
    // Usar la cadena de conexión desde la configuración
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBSomee.com"));
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
