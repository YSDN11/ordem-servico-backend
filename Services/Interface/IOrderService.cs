using ordem_servico_backend.Models;
using ordem_servico_backend.Utils;

namespace ordem_servico_backend.Services.Interface
{
    public interface IOrderService
    {
        Task<(IEnumerable<OrderOutputDto> Items, int Total)> GetPagedAsync(int page, int pageSize, string? search);
        Task<OrderOutputDto?> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<OrderOutputDto?> UpdateChecklistAndPhotosAsync(int id, OrderUpdateDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
