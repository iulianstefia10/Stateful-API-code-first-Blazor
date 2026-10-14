using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClientBlazorSeries.Models.DTO;
using ClientBlazorSeries.Services;

namespace ClientBlazorSeries.ViewModels
{
    public partial class SeriesViewModel : ObservableObject
    {
        private readonly ISerieService _serieService;

        [ObservableProperty]
        private ObservableCollection<SerieDto> _series = new();

        [ObservableProperty]
        private string? _statusMessage;

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
                StatusMessage = "Chargement des séries en cours...";

                var data = await _serieService.GetSeriesAsync(cancellationToken);
                Series = new ObservableCollection<SerieDto>(data);

                StatusMessage = $"{Series.Count} séries chargées.";
            }
            catch (OperationCanceledException)
            {
                StatusMessage = "Le chargement a été annulé par l'utilisateur.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors du chargement : {ex.Message}";
                StatusMessage = null;
            }
        }

        
        [RelayCommand]
        private async Task LoadSeriesSlowAsync(CancellationToken cancellationToken)
        {
            try
            {
                ErrorMessage = null;
                StatusMessage = "Chargement lent en cours (simulation réseau lent)...";

               
                await Task.Delay(4000, cancellationToken);

                var data = await _serieService.GetSeriesAsync(cancellationToken);
                Series = new ObservableCollection<SerieDto>(data);

                StatusMessage = $"{Series.Count} séries chargées (mode lent).";
            }
            catch (OperationCanceledException)
            {
                StatusMessage = "Le chargement lent a été annulé par l'utilisateur.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors du chargement : {ex.Message}";
                StatusMessage = null;
            }
        }
    }
}