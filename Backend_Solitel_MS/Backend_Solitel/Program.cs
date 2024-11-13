using DA.Contexto;
using Microsoft.AspNetCore.Identity;
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

//Conexión a BD
builder.Services.AddDbContext<SeguridadContext>(options =>
{
    // Usar la cadena de conexión desde la configuración
    options.UseSqlServer(builder.Configuration.GetConnectionString("localDB"));
    // Otros ajustes del contexto de base de datos pueden ser configurados aquí, si es necesario
});

builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<SeguridadContext>();


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

app.MapGroup("/identity").MapIdentityApi<IdentityUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();
