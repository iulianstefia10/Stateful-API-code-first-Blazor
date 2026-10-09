using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stateful_API_Code_first.Models.DTO;
using Stateful_API_Code_first.Models.Repository;

namespace Stateful_API_Code_first.Contrôleurs
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesManager _seriesManager;
        private readonly IMapper _mapper;

        public SeriesController(ISeriesManager seriesManager, IMapper mapper)
        {
            _seriesManager = seriesManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SerieDto>>> GetSeries()
        {
            var series = await _seriesManager.GetAllAsync();

            
            var seriesDto = _mapper.Map<IEnumerable<SerieDto>>(series);

            return Ok(seriesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SeriesDetailDto>> GetSerie(int id)
        {
            var serie = await _seriesManager.GetByIdAsync(id);

            if (serie == null)
            {
                return NotFound();
            }

            
            var detailDto = _mapper.Map<SeriesDetailDto>(serie);

            return Ok(detailDto);
        }

        [HttpGet("Networks")]
        public async Task<ActionResult<IEnumerable<NetworkDto>>> GetNetworks()
        {
            var networksStats = await _seriesManager.GetNetworksStatsAsync();
            return Ok(networksStats);
        }
    }
}