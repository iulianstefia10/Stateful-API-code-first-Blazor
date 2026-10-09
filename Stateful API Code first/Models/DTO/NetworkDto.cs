namespace Stateful_API_Code_first.Models.DTO
{
    public class NetworkDto
    {
        public string NetworkName { get; set; } = null!;
        public int NbSeries { get; set; }
        public int TotalEpisodes { get; set; }
    }
}