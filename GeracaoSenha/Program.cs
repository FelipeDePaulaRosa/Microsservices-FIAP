using Microsoft.OpenApi.Models;
using NLog.Web;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

////////var serviceProvider = builder.Services.BuildServiceProvider();
////////var logger = serviceProvider.GetService<NLog.Logger>();
////////builder.Services.AddSingleton(typeof(NLog.Logger), logger);



//NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();








// Personalização do Swagger
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FilmesAPI",
        Version = "v1",
        Description = "Este é um Microsserviço que visa orquestrar o ingresso de pacientes a unidade hospitalar. Gerenciando a senha de atendimento desde sua geração até que o cliente seja atendido no guichê. Além de disponibilizar uma consulta de fila para que o paciente tenha um retorno quanto ao sua posição."
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
