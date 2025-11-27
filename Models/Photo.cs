namespace ordem_servico_backend.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ConteudoBase64 { get; set; } = string.Empty;
        public int SmallIndex { get; set; }

        public Order Order { get; set; }
    }
}
