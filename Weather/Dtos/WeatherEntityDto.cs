using System.Collections.Generic;

namespace Weather.Dtos
{
    public class WeatherEntityDto
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int CurrentT { get; set; }
        public int AverageT { get; set; }
        public int MinT { get; set; }
        public int MaxT { get; set; }
        public ICollection<WeatherHistoryDto> WeatherHistory { get; set; }
    }
}
