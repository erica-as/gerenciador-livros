using GerenciadorLivros.Domain.DTOs.Log;
using GerenciadorLivros.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace GerenciadorLivros.Infrastructure.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly IMongoCollection<LogAtividadeDto> _logs;

        public LogRepository(IConfiguration config)
        {
            var connectionString = config["MONGO_CONNECTION"] ?? config.GetConnectionString("MongoConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("A string de conexão do MongoDB não está configurada. Defina a variável de ambiente MONGO_CONNECTION ou adicione ConnectionStrings:MongoConnection na configuração.");
            }

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("GerenciadorLivrosNoSql");
            _logs = database.GetCollection<LogAtividadeDto>("LogsAtividades");
        }

        public async Task SalvarLogAsync(LogAtividadeDto log) =>
            await _logs.InsertOneAsync(log);
    }
}
