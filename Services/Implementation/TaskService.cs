using ordem_servico_backend.Models;
using ordem_servico_backend.Repositories.Interface;
using ordem_servico_backend.Services.Interface;

namespace ordem_servico_backend.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepo;
        public TaskService(ITaskRepository taskRepo) => _taskRepo = taskRepo;

        public Task<IEnumerable<TaskItem>> GetAllAsync() => _taskRepo.GetAllAsync();

        public Task<TaskItem?> GetByIdAsync(int id) => _taskRepo.GetByIdAsync(id);

        public async Task<TaskItem> CreateAsync(TaskItem task)
        {
            await _taskRepo.AddAsync(task);
            await _taskRepo.SaveChangesAsync();
            return task;
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskItem input)
        {
            var existing = await _taskRepo.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Titulo = input.Titulo;
            await _taskRepo.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _taskRepo.GetByIdAsync(id);
            if (existing == null) return false;

            await _taskRepo.DeleteAsync(existing);
            await _taskRepo.SaveChangesAsync();
            return true;
        }
    }
}
