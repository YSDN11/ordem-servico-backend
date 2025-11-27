using Microsoft.EntityFrameworkCore;
using ordem_servico_backend.Models;
using ordem_servico_backend.Repositories.DatabaseContext;
using ordem_servico_backend.Repositories.Interface;

namespace ordem_servico_backend.Repositories.Implementation
{
    // TaskRepository
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
            await _context.Tasks.AsNoTracking().ToListAsync();

        public Task<TaskItem?> GetByIdAsync(int id) =>
            _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

        public async Task AddAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public Task UpdateAsync(TaskItem task)
        {
            _context.Tasks.Update(task);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
