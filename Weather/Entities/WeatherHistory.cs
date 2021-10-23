using System;
using System.ComponentModel.DataAnnotations;

namespace Weather.Entities
{
    public class WeatherHistory
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int TemperatureC { get; set; }
        public WeatherEntity WeatherEntity { get; set; }
    }
}
