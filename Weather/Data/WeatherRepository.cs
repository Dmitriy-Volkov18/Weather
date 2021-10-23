using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather.Dtos;
using Weather.Entities;
using Weather.Interfaces;

namespace Weather.Data
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly DataContext _context;

        public WeatherRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddWeather(string cityName, AddWeatherDto addWeatherDto)
        {
            var city = _context.Weathers.Include(h => h.WeatherHistory).SingleOrDefault(weather => weather.CityName == cityName);

            if (city is null)
            {
                var weather = new WeatherEntity
                {
                    CityName = cityName,
                    CurrentT = addWeatherDto.TemperatureC,
                    AverageT = addWeatherDto.TemperatureC,
                    MinT = addWeatherDto.TemperatureC,
                    MaxT = addWeatherDto.TemperatureC,
                    WeatherHistory = new List<WeatherHistory>
                    {
                        new WeatherHistory
                        {
                            TemperatureC = addWeatherDto.TemperatureC,
                            Date = addWeatherDto.Date
                        }
                    }
                };

                _context.Weathers.Add(weather);

                return await SaveAllAsync();
            }

            var weatherHistory = new WeatherHistory
            {
                TemperatureC = addWeatherDto.TemperatureC,
                Date = addWeatherDto.Date
            };

            city.WeatherHistory.Add(weatherHistory);
            await _context.SaveChangesAsync();

            UpdateStatistics(city, weatherHistory);

            return await SaveAllAsync();
        }

        public async Task<WeatherEntity> ArchiveWeathers(string cityName)
        {
            var city = await _context.Weathers
                .Include(h => h.WeatherHistory)
                .SingleOrDefaultAsync(c => c.CityName == cityName);
            return city;
        }

        public async Task<bool> DeleteWeather(string cityName, DateTime date, bool deleteOnlyCondition)
        {
            var weather = _context.Weathers.Include(h => h.WeatherHistory).SingleOrDefault(weather => weather.CityName == cityName);

            if (weather is null) return await Task.FromResult(false);

            if(!deleteOnlyCondition)
            {
                _context.Weathers.Remove(weather);
                return await SaveAllAsync();
            }

            var weatherHistory = weather.WeatherHistory.SingleOrDefault(weather => weather.Date == date);

            if (weatherHistory is null) return await Task.FromResult(false);

            weather.WeatherHistory.Remove(weatherHistory);

            return await SaveAllAsync();
        }

        public async Task<ICollection<WeatherHistory>> GetAllWeathersForCity(string cityName)
        {
            var weathers = await _context.Weathers
                .Include(h => h.WeatherHistory)
                .Where(c => c.CityName == cityName)
                .Select(weather => weather.WeatherHistory)
                .SingleOrDefaultAsync();
            return weathers;
        }

        public async Task<CurrentWeatherDto> GetCurrentWeatherForCity(string cityName)
        {
            var currWeather = await _context.Weathers
                .Where(c => c.CityName == cityName)
                .Select(c => new CurrentWeatherDto
                {
                    CurrentT = c.CurrentT,
                    AverageT = c.AverageT,
                    MinT = c.MinT,
                    MaxT = c.MaxT
                })
                .SingleOrDefaultAsync();
            return currWeather;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateWeather(string cityName, DateTime timestamp, UpdateWeatherDto updateWeatherDto)
        {
            var weather = _context.Weathers.Include(h => h.WeatherHistory).SingleOrDefault(weather => weather.CityName == cityName);

            if (weather is null) return await Task.FromResult(false);

            var weatherHistory = weather.WeatherHistory.SingleOrDefault(weather => weather.Date == timestamp);

            if (weatherHistory is null) return await Task.FromResult(false);

            weatherHistory.TemperatureC = updateWeatherDto.TemperatureC;
            weatherHistory.Date = updateWeatherDto.Date;
            await _context.SaveChangesAsync();

            UpdateStatistics(weather, weatherHistory);

            return await SaveAllAsync();
        }

        public void UpdateStatistics(WeatherEntity weather, WeatherHistory weatherHistory)
        {
            weather.CurrentT = weatherHistory.TemperatureC;
            weather.AverageT = (int)weather.WeatherHistory.Average(at => at.TemperatureC);
            weather.MinT = weather.WeatherHistory.Min(mt => mt.TemperatureC);
            weather.MaxT = weather.WeatherHistory.Max(mt => mt.TemperatureC);
        }
    }
}
