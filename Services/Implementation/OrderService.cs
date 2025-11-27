using ordem_servico_backend.Models;
using ordem_servico_backend.Repositories.Interface;
using ordem_servico_backend.Services.Interface;
using ordem_servico_backend.Utils;

namespace ordem_servico_backend.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo) => _orderRepo = orderRepo;

        public async Task<(IEnumerable<OrderOutputDto> Items, int Total)> GetPagedAsync(int page, int pageSize, string? search)
        {
            var (orders, total) = await _orderRepo.GetPagedAsync(page, pageSize, search);

            var result = orders.Select(o => new OrderOutputDto
            {
                Id = o.Id,
                Nome = o.Nome,
                Endereco = o.Endereco,
                Cliente = o.Cliente,
                Descricao = o.Descricao,
                ObservacoesTecnico = o.ObservacoesTecnico,
                TarefasIds = o.TarefasConcluidas.Select(tc => tc.TaskId).Distinct().ToList(),
                TarefasConcluidasIds = o.TarefasConcluidas.Select(tc => tc.TaskId).Distinct().ToList(),
                QtdFotos = o.QtdFotos,
                Fotos = o.Fotos.OrderBy(f => f.SmallIndex)
                               .Select(f => f.ConteudoBase64)
                               .ToList()
            });

            return (result, total);
        }

        public async Task<OrderOutputDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdWithDetailsAsync(id);
            if (order == null) return null;

            return new OrderOutputDto
            {
                Id = order.Id,
                Nome = order.Nome,
                Endereco = order.Endereco,
                Cliente = order.Cliente,
                Descricao = order.Descricao,
                ObservacoesTecnico = order.ObservacoesTecnico,
                TarefasIds = order.TarefasConcluidas.Select(tc => tc.TaskId).Distinct().ToList(),
                TarefasConcluidasIds = order.TarefasConcluidas.Select(tc => tc.TaskId).Distinct().ToList(),
                QtdFotos = order.QtdFotos,
                Fotos = order.Fotos.OrderBy(f => f.SmallIndex)
                                   .Select(f => f.ConteudoBase64)
                                   .ToList()
            };
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();
            return order;
        }

        public async Task<OrderOutputDto?> UpdateChecklistAndPhotosAsync(int id, OrderUpdateDto dto)
        {
            var order = await _orderRepo.GetByIdWithDetailsAsync(id);
            if (order == null) return null;

            order.TarefasConcluidas ??= new List<OrderTasksCompleted>();
            order.TarefasConcluidas.Clear();

            if (dto.TarefasConcluidasIds != null)
            {
                foreach (var idStr in dto.TarefasConcluidasIds.Distinct())
                {
                    if (int.TryParse(idStr, out var taskId))
                    {
                        order.TarefasConcluidas.Add(new OrderTasksCompleted
                        {
                            OrderId = order.Id,
                            TaskId = taskId
                        });
                    }
                }
            }

            order.Fotos ??= new List<Photo>();
            order.Fotos.Clear();

            if (dto.Fotos != null && dto.Fotos.Count > 0)
            {
                var fotosLimitadas = dto.Fotos.Take(3).ToList();
                for (int i = 0; i < fotosLimitadas.Count; i++)
                {
                    order.Fotos.Add(new Photo
                    {
                        OrderId = order.Id,
                        ConteudoBase64 = fotosLimitadas[i],
                        SmallIndex = i
                    });
                }
            }

            order.QtdFotos = dto.QtdFotos;

            await _orderRepo.SaveChangesAsync();

            return await GetByIdAsync(id);
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _orderRepo.GetByIdWithDetailsAsync(id);
            if (order == null) return false;

            await _orderRepo.DeleteAsync(order);
            await _orderRepo.SaveChangesAsync();
            return true;
        }
    }
}
