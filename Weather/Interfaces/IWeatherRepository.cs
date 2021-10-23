using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Dtos;
using Weather.Entities;

namespace Weather.Interfaces
{
    public interface IWeatherRepository
    {
        Task<bool> AddWeather(string cityName, AddWeatherDto addWeatherDto);
        Task<bool> UpdateWeather(string cityName, DateTime timestamp, UpdateWeatherDto updateWeatherDto);
        Task<WeatherEntity> ArchiveWeathers(string cityName);
        Task<ICollection<WeatherHistory>> GetAllWeathersForCity(string cityName);
        Task<CurrentWeatherDto> GetCurrentWeatherForCity(string cityName);
        Task<bool> DeleteWeather(string cityName, DateTime date, bool deleteOnlyCondition);
        Task<bool> SaveAllAsync();
    }
}
