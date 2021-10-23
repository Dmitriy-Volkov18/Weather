using System;
using System.ComponentModel.DataAnnotations;

namespace Weather.Dtos
{
    public class AddWeatherDto
    {
        public int Id { get; set; }
        [Required]
        public int TemperatureC { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
