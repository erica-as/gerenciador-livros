using GerenciadorLivros.API.Configuration;
using GerenciadorLivros.Domain.Interfaces;
using GerenciadorLivros.Infrastructure.Context;
using GerenciadorLivros.Infrastructure.Repositories;
using GerenciadorLivros.Service.Interfaces;
using GerenciadorLivros.Service.Services;
using Microsoft.EntityFrameworkCore;

MongoSerializationConfig.ConfigureGuidRepresentation();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddOpenApi(); 
builder.Services.AddSwaggerGen(); 

var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "(localdb)\\MSSQLLocalDB";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "LivroDb";
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var mongoConn = Environment.GetEnvironmentVariable("MONGO_CONNECTION") ?? "mongodb://admin:senha_mongo@localhost:27017";


string connectionString;
if (string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword))
{
    connectionString = $"Server={dbServer};Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true";
}
else
{
    connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True";
}


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ILivroService, LivroService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("Executando migrations...");
        db.Database.Migrate();
        logger.LogInformation("Banco de dados criado/atualizado com sucesso!");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "ERRO ao executar migrations: {Message}", ex.Message);
        throw;
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

