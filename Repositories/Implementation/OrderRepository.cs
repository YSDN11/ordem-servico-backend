using Microsoft.EntityFrameworkCore;
using ordem_servico_backend.Models;
using ordem_servico_backend.Repositories.DatabaseContext;
using ordem_servico_backend.Repositories.Interface;

namespace ordem_servico_backend.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) => _context = context;

        public async Task<(IEnumerable<Order> Orders, int Total)> GetPagedAsync(int page, int pageSize, string? search)
        {
            var query = _context.Orders
                .Include(o => o.TarefasConcluidas)
                .Include(o => o.Fotos)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim().ToLower();
                query = query.Where(o => o.Nome.ToLower().Contains(term));
            }

            var total = await query.CountAsync();

            var orders = await query
                .OrderBy(o => o.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orders, total);
        }

        public Task<Order?> GetByIdWithDetailsAsync(int id) =>
            _context.Orders
                .Include(o => o.TarefasConcluidas)
                .Include(o => o.Fotos)
                .FirstOrDefaultAsync(o => o.Id == id);

        public Task<Order?> GetByIdAsync(int id) =>
            _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
