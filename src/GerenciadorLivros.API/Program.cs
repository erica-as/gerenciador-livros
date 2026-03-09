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

// Configurar o Banco de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar a Injeção de Dependência do Repositório
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<ILivroService, LivroService>();

// --- 2. CRIAÇÃO DA APLICAÇÃO ---

var app = builder.Build();

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