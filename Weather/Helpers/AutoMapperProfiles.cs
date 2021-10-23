using AutoMapper;
using Weather.Dtos;
using Weather.Entities;

namespace Weather.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<WeatherEntity, WeatherEntityDto>();
            CreateMap<WeatherHistory, WeatherHistoryDto>();
        }
    }
}
