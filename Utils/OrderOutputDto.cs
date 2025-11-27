namespace ordem_servico_backend.Utils
{
    public class OrderOutputDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cliente { get; set; }
        public string Descricao { get; set; }
        public string ObservacoesTecnico { get; set; }

        public List<int> TarefasIds { get; set; } = new();
        public List<int> TarefasConcluidasIds { get; set; } = new();

        public int QtdFotos { get; set; }
        public List<string> Fotos { get; set; } = new();
    }
}
