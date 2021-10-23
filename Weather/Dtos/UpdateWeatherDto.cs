using System;
using System.ComponentModel.DataAnnotations;


namespace Weather.Dtos
{
    public class UpdateWeatherDto
    {
        [Required]
        public int TemperatureC { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
