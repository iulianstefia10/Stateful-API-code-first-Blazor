namespace Stateful_API_Code_first.Models.DTO
{
    public class SeriesDetailDto
    {
        public int SerieId { get; set; }
        public string Title { get; set; } = null!;
        public string? Resume { get; set; }
        public int? NbSaisons { get; set; }
        public int? NbEpisodes { get; set; }
        public int? CreationYear { get; set; }
        public string? Network { get; set; }
        public double AverageEpisodesPerSeason { get; set; }
        public bool EstEnCours { get; set; }
    }
}