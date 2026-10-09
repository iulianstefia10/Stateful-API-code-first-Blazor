using ClientBlazorSeries.Models.DTO;
using ClientBlazorSeries.Pages;
using ClientBlazorSeries.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ClientBlazorSeries.ViewModels;

public partial class SeriesViewModel : ObservableObject
{
    private readonly ISerieService _serieService;

    [ObservableProperty]
    private ObservableCollection<SerieDto> _series = new();

    [ObservableProperty]
    private string? _errorMessage;

    public SeriesViewModel(ISerieService serieService)
    {
        _serieService = serieService;
    }

    [RelayCommand]
    private async Task LoadSeriesAsync(CancellationToken cancellationToken)
    {
        try
        {
            ErrorMessage = null;
            var data = await _serieService.GetSeriesAsync(cancellationToken);
            if (data != null)
            {
                Series = new ObservableCollection<SerieDto>(data);
            }
        }
        catch (OperationCanceledException)
        {
            ErrorMessage = "Le chargement a été annulé par l'utilisateur.";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erreur lors du chargement : {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task LoadSeriesSlowAsync(CancellationToken cancellationToken)
    {
        try
        {
            ErrorMessage = null;
            // Simulation d'une latence réseau
            await Task.Delay(4000, cancellationToken);
            var data = await _serieService.GetSeriesAsync(cancellationToken);
            if (data != null)
            {
                Series = new ObservableCollection<SerieDto>(data);
            }
        }
        catch (OperationCanceledException)
        {
            ErrorMessage = "Le chargement simulé lent a été annulé.";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erreur lors du chargement : {ex.Message}";
        }
    }
}