using GerenciadorLivros.Domain.Interfaces;
using GerenciadorLivros.Infrastructure.Context;
using GerenciadorLivros.Infrastructure.Repositories;
using GerenciadorLivros.Service.Interfaces;
using GerenciadorLivros.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURAÇÕES DOS SERVIÇOS (Tudo que usa 'builder.Services') ---

builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Nativo do .NET 9/10
builder.Services.AddSwaggerGen(); // Interface Visual do Swagger

// Configurar Connection String com variáveis de ambiente
var dbServer = Environment.GetEnvironmentVariable("DB_SERVER") ?? "(localdb)\\MSSQLLocalDB";
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "LivroDb";
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

string connectionString;
if (string.IsNullOrEmpty(dbUser) || string.IsNullOrEmpty(dbPassword))
{
    // Desenvolvimento local com LocalDB
    connectionString = $"Server={dbServer};Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true";
}
else
{
    // Docker/Produção com SQL Server
    connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True";
}

// Configurar o Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configurar a Injeção de Dependência do Repositório
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILivroService, LivroService>();

// --- 2. CRIAÇÃO DA APLICAÇÃO ---

var app = builder.Build();

// Isso força a criação do banco e das tabelas assim que a API sobe no Docker
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation("🔄 Executando migrations...");
        db.Database.Migrate();
        logger.LogInformation("✅ Banco de dados criado/atualizado com sucesso!");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "❌ ERRO ao executar migrations: {Message}", ex.Message);
        throw; // Re-lança para não deixar a API subir sem banco
    }
}

// --- 3. CONFIGURAÇÃO DO PIPELINE DE EXECUÇÃO (Tudo que usa 'app') ---

// Ativar o Swagger apenas em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   // Gera o arquivo JSON
    app.UseSwaggerUI(); // Cria a página visual para testar
}

app.UseHttpsRedirection();

// Mapeia os seus Controllers para que os endpoints funcionem
app.MapControllers();

app.Run();