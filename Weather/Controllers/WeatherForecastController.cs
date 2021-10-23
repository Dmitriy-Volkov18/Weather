using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Weather.Dtos;
using Weather.Interfaces;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWeatherRepository _weatherRepository;

        public WeatherForecastController(IMapper mapper, IWeatherRepository weatherRepository)
        {
            _mapper = mapper;
            _weatherRepository = weatherRepository;
        }

        [HttpPost("add-weather/{cityName}")]
        public async Task<IActionResult> AddWeather(string cityName, AddWeatherDto addTemperatureDto)
        {
            var result = await _weatherRepository.AddWeather(cityName, addTemperatureDto);

            if (result) return Ok();

            return BadRequest();
        }

        [HttpPut("update-weather/{cityName}/{timestamp}")]
        public async Task<IActionResult> UpdateWeather(string cityName, DateTime timestamp, UpdateWeatherDto updateWeatherDto)
        {
            var result = await _weatherRepository.UpdateWeather(cityName, timestamp, updateWeatherDto);

            if (result) return Ok();

            return BadRequest();
        }

        [HttpGet("currentWeather/{cityName}")]
        public async Task<ActionResult<CurrentWeatherDto>> GetCurrentWeatherForCity(string cityName)
        {
            var city = await _weatherRepository.GetCurrentWeatherForCity(cityName);
            var cityToReturn = _mapper.Map<CurrentWeatherDto>(city);

            if (cityToReturn is null) return NotFound();

            return cityToReturn;
        }

        [HttpGet("{cityName}")]
        public async Task<ActionResult<WeatherHistoryDto[]>> GetAllWeathersForCity(string cityName)
        {
            var weathers = await _weatherRepository.GetAllWeathersForCity(cityName);
            var weathersToReturn = _mapper.Map<WeatherHistoryDto[]>(weathers);

            if (weathersToReturn is null) return NotFound();

            return weathersToReturn;
        }

        [HttpDelete("delete-weather/{cityName}/{timestamp}")]
        public async Task<IActionResult> DeleteWeather(string cityName, DateTime timestamp)
        {
            var result = await _weatherRepository.DeleteWeather(cityName, timestamp, true);

            if (result) return Ok();

            return BadRequest();
        }
    }
}
