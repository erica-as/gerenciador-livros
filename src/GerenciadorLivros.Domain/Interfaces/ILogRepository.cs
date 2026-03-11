using GerenciadorLivros.Domain.DTOs.Log;

namespace GerenciadorLivros.Domain.Interfaces
{
    public interface ILogRepository
    {
        Task SalvarLogAsync(LogAtividadeDto log);
    }
}
