using ClientBlazorSeries.Models.DTO;
using ClientBlazorSeries.Services;
using ClientBlazorSeries.ViewModels;
using Moq;
using Stateful_API_Code_first.Models.DTO;
using Xunit;

namespace APISeriesTests;

public class SeriesViewModelTests
{
    [Fact]
    public async Task LoadSeriesCommand_ShouldPopulateSeries_WhenServiceReturnsData()
    {
        // Arrange
        var mockService = new Mock<ISerieService>();
        var sampleSeries = new List<SerieDto>
        {
            new() { SerieId = 1, Titre = "Breaking Bad", NbSaisons = 5, NbEpisodes = 62 },
            new() { SerieId = 2, Titre = "Chernobyl", NbSaisons = 1, NbEpisodes = 5 }
        };

        mockService.Setup(s => s.GetSeriesAsync(It.IsAny<CancellationToken>()))
                   .ReturnsAsync(sampleSeries);

        var viewModel = new SeriesViewModel(mockService.Object);

        // Act
        await viewModel.LoadSeriesCommand.ExecuteAsync(null);

        // Assert
        Assert.NotNull(viewModel.Series);
        Assert.Equal(2, viewModel.Series.Count);
        Assert.Equal("Breaking Bad", viewModel.Series[0].Titre);
        Assert.Null(viewModel.ErrorMessage);
    }
}