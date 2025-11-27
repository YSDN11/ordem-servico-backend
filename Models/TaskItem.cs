namespace ordem_servico_backend.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;

        public ICollection<OrderTasksCompleted> OrdersCompleted { get; set; } = new List<OrderTasksCompleted>();
    }
}
