using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Weather.Entities
{
    public class WeatherEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public int CurrentT { get; set; }
        public int AverageT { get; set; }
        public int MinT { get; set; }
        public int MaxT { get; set; }
        public ICollection<WeatherHistory> WeatherHistory { get; set; }
    }
}
