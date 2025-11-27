using ordem_servico_backend.Models;

namespace ordem_servico_backend.Repositories.Interface
{
    public interface IOrderRepository
    {
        Task<(IEnumerable<Order> Orders, int Total)> GetPagedAsync(int page, int pageSize, string? search);
        Task<Order?> GetByIdWithDetailsAsync(int id);
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Order order);
        Task SaveChangesAsync();
    }
}
