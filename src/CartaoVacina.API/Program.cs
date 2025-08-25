using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CartaoVacina.API.Middleware;
using CartaoVacina.Application.Commands.Pessoas;
using CartaoVacina.Application.Interfaces;
using CartaoVacina.Application.Mappings;
using CartaoVacina.Infrastructure.Data.Contexts;
using CartaoVacina.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Entity Framework
builder.Services.AddDbContext<CartaoVacinaDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    
    // Se estiver em ambiente Docker, adiciona a senha da variável de ambiente
    if (builder.Environment.EnvironmentName == "Docker")
    {
        var saPassword = Environment.GetEnvironmentVariable("SA_PASSWORD");
        connectionString += $";Password={saPassword}";
    }
    
    options.UseSqlServer(connectionString);
});

// MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreatePessoaCommand).Assembly));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreatePessoaCommand).Assembly);

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Repositories
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<IVacinaRepository, VacinaRepository>();
builder.Services.AddScoped<IVacinacaoRepository, VacinacaoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "Vaccination Card API", 
        Version = "v1",
        Description = "API para gerenciamento de cartão de vacinação"
    });

    // Include XML comments
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Vaccination Card API v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

// Custom middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// CORS
app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CartaoVacinaDbContext>();
    context.Database.EnsureCreated();
}

app.Run();