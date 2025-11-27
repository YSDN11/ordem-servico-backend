namespace ordem_servico_backend.Utils
{
    public class OrderUpdateDto
    {
        public List<string> TarefasConcluidasIds { get; set; } = new();
        public List<string> Fotos { get; set; } = new();
        public int QtdFotos { get; set; }
        public string? ObservacoesTecnico { get; set; }
    }
}
