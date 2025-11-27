using ordem_servico_backend.Models;

namespace ordem_servico_backend.Services.Interface
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<TaskItem?> UpdateAsync(int id, TaskItem input);
        Task<bool> DeleteAsync(int id);
    }
}
