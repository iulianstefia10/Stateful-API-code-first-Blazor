using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClientBlazorSeries.Models.DTO;
using ClientBlazorSeries.Services;

namespace ClientBlazorSeries.ViewModels;

public partial class SerieDetailViewModel : ObservableObject
{
    private readonly ISerieService _serieService;

    [ObservableProperty]
    private SerieDetailDto? _serie;

    [ObservableProperty]
    private string? _statusMessage;

    [ObservableProperty]
    private bool _isSuccess;

    public SerieDetailViewModel(ISerieService serieService)
    {
        _serieService = serieService;
    }

    [RelayCommand]
    public async Task LoadDetailAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            StatusMessage = null;
            Serie = await _serieService.GetSerieByIdAsync(id, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            StatusMessage = "Chargement des détails annulé.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Erreur lors du chargement : {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task SaveAsync(CancellationToken cancellationToken)
    {
        if (Serie == null) return;

        try
        {
            StatusMessage = null;
            IsSuccess = false;
            bool success = await _serieService.UpdateSerieAsync(Serie.SerieId, Serie, cancellationToken);
            if (success)
            {
                IsSuccess = true;
                StatusMessage = "Série mise à jour avec succès.";
            }
            else
            {
                StatusMessage = "La mise à jour a échoué sur le serveur.";
            }
        }
        catch (OperationCanceledException)
        {
            StatusMessage = "La sauvegarde a été annulée.";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Erreur de sauvegarde : {ex.Message}";
        }
    }
}