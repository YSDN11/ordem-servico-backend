namespace ordem_servico_backend.Models
{
    public class OrderTasksCompleted
    {
        public int OrderId { get; set; }
        public int TaskId { get; set; }

        public Order Order { get; set; }
        public TaskItem Task { get; set; }
    }
}
