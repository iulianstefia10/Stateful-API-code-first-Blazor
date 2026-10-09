using System.ComponentModel.DataAnnotations;

namespace ClientBlazorSeries.Models.DTO;

public class SerieDetailDto
{
    public int SerieId { get; set; }

    [Required(ErrorMessage = "Le titre est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le titre ne peut pas dépasser 100 caractères.")]
    public string Titre { get; set; } = string.Empty;

    public string? Resume { get; set; }

    [Range(1, 100, ErrorMessage = "Le nombre de saisons doit être compris entre 1 et 100.")]
    public int? NbSaisons { get; set; }

    [Range(1, 5000, ErrorMessage = "Le nombre d'épisodes doit être valide.")]
    public int? NbEpisodes { get; set; }

    [Range(1900, 2100, ErrorMessage = "L'année de création doit être une année valide.")]
    public int? AnneeCreation { get; set; }

    public string? Network { get; set; }

    // Champs calculés demandés par le sujet
    public double AverageEpisodesPerSeason =>
        (NbSaisons.HasValue && NbSaisons.Value > 0 && NbEpisodes.HasValue)
            ? Math.Round((double)NbEpisodes.Value / NbSaisons.Value, 1)
            : 0;

    public bool IsInProgress { get; set; }
}