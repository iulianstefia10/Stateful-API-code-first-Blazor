using System;
using APIfilms.Models.EntityFramework;
using AutoMapper;
using Stateful_API_Code_first.Models.DTO;

using Stateful_API_Code_first.Models.Repository;

namespace Stateful_API_Code_first.Profiles
{
    public class SeriesProfile : Profile
    {
        public SeriesProfile()
        {
            CreateMap<Serie, SerieDto>();

            CreateMap<Serie, SeriesDetailDto>()
                .ForMember(dest => dest.AverageEpisodesPerSeason, opt => opt.MapFrom(src =>
                    (src.NbSaisons.HasValue && src.NbSaisons > 0 && src.NbEpisodes.HasValue)
                    ? Math.Round((double)src.NbEpisodes.Value / src.NbSaisons.Value, 2)
                    : 0.0))
                .ForMember(dest => dest.EstEnCours, opt => opt.MapFrom(src =>
                    src.CreationYear.HasValue && src.CreationYear >= 2024));
        }
    }
}