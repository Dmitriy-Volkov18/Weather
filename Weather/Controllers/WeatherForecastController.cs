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

            if (result) return Created("msg", "New weather was created successfully");

            return BadRequest("There is already existing weather with same date");
        }

        [HttpPut("update-weather/{cityName}/{timestamp}")]
        public async Task<IActionResult> UpdateWeather(string cityName, DateTime timestamp, UpdateWeatherDto updateWeatherDto)
        {
            var result = await _weatherRepository.UpdateWeather(cityName, timestamp, updateWeatherDto);

            if (result) return Ok("Weather was successfully updated");

            return BadRequest("Something went wrong");
        }

        [HttpGet("currentWeather/{cityName}")]
        public async Task<ActionResult<CurrentWeatherDto>> GetCurrentWeatherForCity(string cityName)
        {
            var city = await _weatherRepository.GetCurrentWeatherForCity(cityName);
            var cityToReturn = _mapper.Map<CurrentWeatherDto>(city);

            if (cityToReturn is null) return NotFound("The city doesn`t exist. Add it before view info");

            return cityToReturn;
        }

        [HttpGet("{cityName}")]
        public async Task<ActionResult<WeatherHistoryDto[]>> GetAllWeathersForCity(string cityName)
        {
            var weathers = await _weatherRepository.GetAllWeathersForCity(cityName);
            var weathersToReturn = _mapper.Map<WeatherHistoryDto[]>(weathers);

            if (weathersToReturn.Length == 0) return NotFound("There are no existing weathers for that city");

            return weathersToReturn;
        }

        [HttpGet("archive-weathers/{cityName}/{startDate}/{endDate}")]
        public ActionResult<WeatherHistoryDto[]> ArchiveWeathers(string cityName, DateTime startDate, DateTime endDate)
        {
            var weatherHistories = _weatherRepository.ArchiveWeathers(cityName, startDate, endDate);
            var weathersToReturn = _mapper.Map<WeatherHistoryDto[]>(weatherHistories);

            if (weathersToReturn.Length == 0) return NotFound("There are no any weathers for that period of time");

            return Ok(weathersToReturn);
        }

        [HttpDelete("delete-weather/{cityName}/{timestamp}")]
        public async Task<IActionResult> DeleteWeather(string cityName, DateTime timestamp)
        {
            var result = await _weatherRepository.DeleteWeather(cityName, timestamp, true);

            if (result) return Ok("Weather was successfully deleted");

            return BadRequest("Something went wrong");
        }
    }
}
