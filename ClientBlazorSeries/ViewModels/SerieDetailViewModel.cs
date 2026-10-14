using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ClientBlazorSeries.Models.DTO;
using ClientBlazorSeries.Services;

namespace ClientBlazorSeries.ViewModels
{
    public partial class SerieDetailViewModel : ObservableObject
    {
        private readonly ISerieService _serieService;

        [ObservableProperty]
        private SerieDetailDto? _serie;

        [ObservableProperty]
        private string? _statusMessage;

        [ObservableProperty]
        private string? _errorMessage;

        
        [ObservableProperty]
        private bool _isSuccess;

        public SerieDetailViewModel(ISerieService serieService)
        {
            _serieService = serieService;
        }

        
        [RelayCommand]
        private async Task LoadDetailAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                IsSuccess = false;
                ErrorMessage = null;
                StatusMessage = "Chargement des détails...";
                Serie = await _serieService.GetSerieByIdAsync(id, cancellationToken);
                StatusMessage = null;
            }
            catch (OperationCanceledException)
            {
                StatusMessage = "Chargement des détails annulé.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur : {ex.Message}";
                StatusMessage = null;
            }
        }

        
        [RelayCommand]
        private async Task LoadSerieAsync(int id, CancellationToken cancellationToken)
        {
            await LoadDetailAsync(id, cancellationToken);
        }

      
        [RelayCommand]
        private async Task SaveAsync(CancellationToken cancellationToken)
        {
            if (Serie == null) return;

            try
            {
                IsSuccess = false;
                ErrorMessage = null;
                StatusMessage = "Enregistrement des modifications...";

                await Task.Delay(1500, cancellationToken);

             
                var success = await _serieService.UpdateSerieAsync(Serie.SerieId, Serie, cancellationToken);
                if (success)
                {
                    IsSuccess = true;
                    StatusMessage = "Série mise à jour avec succès !";
                }
                else
                {
                    IsSuccess = false;
                    ErrorMessage = "Échec de la mise à jour de la série.";
                    StatusMessage = null;
                }
            }
            catch (OperationCanceledException)
            {
                StatusMessage = "La sauvegarde a été annulée.";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Erreur lors de la sauvegarde : {ex.Message}";
                StatusMessage = null;
            }
        }
    }
}