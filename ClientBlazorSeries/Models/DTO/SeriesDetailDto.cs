using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientBlazorSeries.Models.DTO
{
    public class SerieDetailDto
    {
        public int SerieId { get; set; }

        [Required(ErrorMessage = "Le titre de la série est obligatoire.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Le titre doit contenir entre 2 et 100 caractères.")]
        [JsonPropertyName("titre")]
        public string Titre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le résumé est obligatoire.")]
        public string Summary { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Le nombre de saisons doit être compris entre 1 et 100.")]
        public int Seasons { get; set; }

        [Range(1, 5000, ErrorMessage = "Le nombre d'épisodes doit être supérieur à 0.")]
        public int Episodes { get; set; }

        [Range(1900, 2100, ErrorMessage = "L'année de création doit être valide (entre 1900 et 2100).")]
        public int CreationYear { get; set; }

        public bool IsInProgress { get; set; }

        public double AverageEpisodesPerSeason => Seasons > 0 ? Math.Round((double)Episodes / Seasons, 1) : 0;

        public string? NetworkName { get; set; }
    }
}