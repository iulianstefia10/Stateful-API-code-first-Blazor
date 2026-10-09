namespace Stateful_API_Code_first.Models.DTO
{
    public class SerieDto
    {
        public int SerieId { get; set; }
        public string Title { get; set; } = null!;
        public int? CreationYear { get; set; }
        public string? Network { get; set; }
    }
}