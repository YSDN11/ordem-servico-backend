namespace ordem_servico_backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string ObservacoesTecnico { get; set; } = string.Empty;
        public int QtdFotos { get; set; }

        public ICollection<OrderTasksCompleted> TarefasConcluidas { get; set; } = new List<OrderTasksCompleted>();
        public ICollection<Photo> Fotos { get; set; } = new List<Photo>();
    }
}
