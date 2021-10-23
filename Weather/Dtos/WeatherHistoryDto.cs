using System;

namespace Weather.Dtos
{
    public class WeatherHistoryDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
    }
}
