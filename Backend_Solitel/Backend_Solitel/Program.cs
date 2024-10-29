using BW.CU;
using BW.Interfaces.BW;
using BW.Interfaces.DA;
using DA.Acciones;
using DA.Contexto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

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

builder.Services.AddTransient<IGestionarSolicitudAnalistaBW, GestionarSolicitudAnalistaBW>();
builder.Services.AddTransient<IGestionarSolicitudAnalistaDA, GestionarSolicitudAnalistaDA>();

builder.Services.AddTransient<IGestionarTipoDatoBW, GestionarTipoDatoBW>();
builder.Services.AddTransient<IGestionarTipoDatoDA, GestionarTipoDatoDA>();
builder.Services.AddTransient<IGestionarTipoAnalisisBW, GestionarTipoAnalisisBW>();
builder.Services.AddTransient<IGestionarTipoAnalisisDA, GestionarTipoAnalisisDA>();

builder.Services.AddTransient<IGestionarObjetivoAnalisisBW, GestionarObjetivoAnalisisBW>();
builder.Services.AddTransient<IGestionarObjetivoAnalisisDA, GestionarObjetivoAnalisisDA>();

builder.Services.AddTransient<IGestionarSolicitudProveedorBW, GestionarSolicitudProveedorBW>();
builder.Services.AddTransient<IGestionarSolicitudProveedorDA, GestionarSolicitudProveedorDA>();

builder.Services.AddTransient<IGestionarRequerimientoProveedorBW, GestionarRequerimientoProveedorBW>();
builder.Services.AddTransient<IGestionarRequerimientoProveedorDA, GestionarRequerimientoProveedorDA>();

builder.Services.AddTransient<IGestionarProveedorBW, GestionarProveedorBW>();
builder.Services.AddTransient<IGestionarProveedorDA, GestionarProveedorDA>();

builder.Services.AddTransient<IGestionarOficinaBW, GestionarOficinaBW>();
builder.Services.AddTransient<IGestionarOficinaDA, GestionarOficinaDA>();

builder.Services.AddTransient<IGestionarArchivoDA,GestionarArchivoDA>();
builder.Services.AddTransient<IGestionarArchivoBW, GestionarArchivoBW>();

builder.Services.AddTransient<IGestionarObjetivoAnalisisBW, GestionarObjetivoAnalisisBW>();
builder.Services.AddTransient<IGestionarObjetivoAnalisisDA, GestionarObjetivoAnalisisDA>();

builder.Services.AddTransient<IGestionarEstadoDA, GestionarEstadoDA>();
builder.Services.AddTransient<IGestionarEstadoBW, GestionarEstadoBW>();


//Conexi�n a BD
builder.Services.AddDbContext<SolitelContext>(options =>
{
    // Usar la cadena de conexi�n desde la configuraci�n
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBSomee.com"));
    // Otros ajustes del contexto de base de datos pueden ser configurados aqu�, si es necesario
});
var app = builder.Build();

//configuracion de cores
app.UseCors("AllowOrigin");
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

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
