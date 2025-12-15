using Application.DTOs.Categoria;
using Application.DTOs.Transacao;
using Application.DTOs.Usuario;
using Application.Services;
using Application.Validators.Categorias;
using Application.Validators.Transacoes;
using Application.Validators.Usuarios;

using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

using FluentValidation;

using HomeControl.Extensions;

using Infrastructure.Ioc;



var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddUserSecrets<Program>(optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddControllers();
builder.Services.AddOpenApi();


builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


builder.Services.AddSwaggerGen();

builder.Services.AddMappings();

builder.Services.AddInfraServices(builder.Configuration);

// Register Validators

//Usuarios
builder.Services.AddScoped<IValidator<UsuarioCreateDto>, UsuarioCreateDtoValidator>();
builder.Services.AddScoped<IValidator<UsuarioUpdateDto>, UsuarioUpdateDtoValidator>();

//Categorias
builder.Services.AddScoped<IValidator<CategoriaCreateDto>, CategoriaCreateDtoValidator>();
builder.Services.AddScoped<IValidator<CategoriaUpdateDto>, CategoriaUpdateDtoValidator>();

//Transacoes
builder.Services.AddScoped<IValidator<TransacaoCreateDto>, TransacaoCreateDtoValidator>();



var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                $"HomeControl API {description.GroupName}");
        }
    });
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
