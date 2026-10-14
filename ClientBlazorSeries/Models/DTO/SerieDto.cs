namespace ClientBlazorSeries.Models.DTO
{
    public class SerieDto
    {
        public int SerieId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Resume { get; set; }
        public int NbSaisons { get; set; }
        public int NbEpisodes { get; set; }
        public int CreationYear { get; set; }
        public string? Network { get; set; }
    }
}